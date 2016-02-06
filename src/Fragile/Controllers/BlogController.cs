using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Fragile.Models;
using Fragile.Attributes;
using Fragile.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Fragile.Controllers
{
    public class BlogController : Controller
    {
        public ApplicationDbContext DbContext
        {
            get;
        }

        public AuthenticationService Authentication
        {
            get;
        }

        public BlogController(ApplicationDbContext dbContext, AuthenticationService authenticationService)
        {
            this.DbContext = dbContext;
            this.Authentication = authenticationService;
        }

        public IActionResult Index()
        {
            return View(DbContext.Blog.Where(b => b.PostAfterDate < DateTime.Now));
        }

        // /Blog/Author/Gage Orsburn
        [Route("/Blog/Author/{Author}")]
        public IActionResult By(string Author)
        {
            return View("Index", DbContext.Blog.Where(b => b.AuthorName == Author && b.PostAfterDate < DateTime.Now));
        }

        // /Blog/Article/Id
        [Route("/Blog/Article/{Title}")]
        public IActionResult Article(string Title)
        {
            var post = DbContext.Blog.Where(b => b.Title == Title).FirstOrDefault();

            if (post == null)
                return HttpNotFound();

            return View(post);
        }

        [HttpGet]
        [RequireAuthentication]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequireAuthentication]
        public async Task<IActionResult> Create(Blog blog)
        {
            blog.AuthorName = Authentication.AuthorizedMember?.Name;

            if (blog.PostAfterDate == null)
                blog.PostAfterDate = DateTime.Now;

            DbContext.Blog.Add(blog);
            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [RequireAuthentication]
        public IActionResult Update()
        {
            return View();
        }

        [HttpGet]
        [RequireAuthentication]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
