using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using InteriorDesignWebsite.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;

namespace InteriorDesignWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _connectionString;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public IActionResult Index() => View();
        public IActionResult About() => View();
        public IActionResult Projects() => View();
        public IActionResult Contact() => View();
        public IActionResult Consultation() => View();
        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            string adminUsername = "admin";
            string adminPassword = "password";

            if (username == adminUsername && password == adminPassword)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewData["LoginError"] = "Invalid username or password.";
                return View();
            }
        }


        public IActionResult Blog()
        {
            List<BlogPost> blogPosts = new List<BlogPost>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT ImageUrl, CreatedAt FROM BlogPosts"; // Select all needed columns.
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        blogPosts.Add(new BlogPost
                        {
                            ImageUrl = reader["ImageUrl"].ToString(),
                            CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                        });
                    }
                }
            }
            return View(blogPosts);
        }


        public async Task<IActionResult> Dashboard()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7131/api/contactform");

            List<ContactForm> contactForms = new List<ContactForm>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                contactForms = JsonConvert.DeserializeObject<List<ContactForm>>(content);
            }

            // Fetch blog images from database
            List<string> imageUrls = new List<string>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT ImageUrl FROM BlogPosts";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        imageUrls.Add(reader["ImageUrl"].ToString());
                    }
                }
            }

            // ✅ Pass the correct model type
            var viewModel = new DashboardViewModel
            {
                ContactForms = contactForms,
                ImageUrls = imageUrls
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadBlogPost(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                TempData["Error"] = "Please upload an image.";
                return RedirectToAction("Dashboard");
            }

            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            string ImageUrl = "/images/" + uniqueFileName;

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO BlogPosts (ImageUrl, CreatedAt) VALUES (@ImageUrl, GETDATE())";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@ImageUrl", ImageUrl);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            TempData["Success"] = "Blog post uploaded successfully!";
            return RedirectToAction("Blog");
        }
    }
}
