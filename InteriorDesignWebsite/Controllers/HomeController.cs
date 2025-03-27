using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InteriorDesignWebsite.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace InteriorDesignWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        // Inject IHttpClientFactory to make API calls
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        // GET: Home/Index
        public IActionResult Index()
        {
            return View();
        }

        // GET: Home/About
        public IActionResult About()
        {
            return View();
        }

        // GET: Home/Projects
        public IActionResult Projects()
        {
            return View();
        }

        // GET: Home/Blog
        public IActionResult Blog()
        {
            return View();
        }

        // GET: Home/Contact
        public IActionResult Contact()
        {
            return View();
        }

        // GET: Home/Consultation
        public IActionResult Consultation()
        {
            return View();
        }

        // POST: Home/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Hardcoded credentials for admin
            string adminUsername = "admin";
            string adminPassword = "password"; // Change this to your actual password

            if (username == adminUsername && password == adminPassword)
            {
                // Redirect to the Dashboard after successful login
                return RedirectToAction("Dashboard");
            }
            else
            {
                // Display error message for incorrect credentials
                ViewData["LoginError"] = "Invalid username or password.";
                return View();
            }
        }

        // GET: Home/Login
        public IActionResult Login()
        {
            return View();
        }

        // Dashboard action - fetch contact form data from the API and display it
        public async Task<IActionResult> Dashboard()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:7131/api/contactform");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contactForms = JsonConvert.DeserializeObject<List<ContactForm>>(content);

                // Pass the data to the Dashboard view
                return View(contactForms);
            }
            else
            {
                // If API call fails, return an empty list
                return View(new List<ContactForm>());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
