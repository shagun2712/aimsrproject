using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using InteriorDesignWebsite.Models; // Ensure you have the correct namespace for your models
using InteriorDesignWebsite.Data; // Ensure this matches your DbContext namespace

namespace InteriorDesignWebsite.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;

        public BlogController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BlogController
        public IActionResult Index()
        {
            var blogPosts = _context.BlogPosts
                .OrderByDescending(b => b.CreatedAt) // Fetch blog posts ordered by latest first
                .ToList();

            return View(blogPosts);
        }

        // GET: BlogController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BlogPost blogPost)
        {
            if (ModelState.IsValid)
            {
                blogPost.CreatedAt = DateTime.Now;
                _context.BlogPosts.Add(blogPost);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(blogPost);
        }
    }
}
