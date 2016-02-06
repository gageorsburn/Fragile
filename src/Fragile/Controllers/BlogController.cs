using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Fragile.Models;
using Fragile.Attributes;
using Fragile.Services;

namespace Fragile.Controllers
{
    public class BlogController : BasicController
    {
        public BlogController(ApplicationDbContext dbContext, AuthenticationService authenticationService) : 
            base(dbContext, authenticationService) { }

        public IActionResult Index()
        {
            return View(DbContext.Blog.Where(article => article.PostDate < DateTime.Now));
        }

        [Route("/Blog/Author/{Author}")]
        public IActionResult By(string Author)
        {
            return View("Index", 
                DbContext.Blog.Where(article => article.AuthorName == Author && article.PostDate < DateTime.Now));
        }

        [Route("/Blog/Article/{Title}")]
        public IActionResult Article(string Title)
        {
            var post = DbContext.Blog.Where(article => article.Title == Title).FirstOrDefault();

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
            blog.AuthorName = AuthenticationService.AuthorizedMember?.Name;

            if (blog.PostDate == null)
                blog.PostDate = DateTime.Now;

            DbContext.Blog.Add(blog);
            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("Blog/Update/{Id}")]
        [HttpGet]
        [RequireAuthentication]
        public IActionResult Update(int Id)
        {
            return View(DbContext.Blog.Where(article => article.Id == Id).FirstOrDefault());
        }

        [Route("Blog/Update/{Id}")]
        [HttpPost]
        [RequireAuthentication]
        public async Task<IActionResult> Update(int Id, Blog blog)
        {
            Blog updateBlog = DbContext.Blog.Where(article => article.Id == Id).FirstOrDefault();

            updateBlog.Title = blog.Title;
            updateBlog.Body = blog.Body;
            updateBlog.PostDate = blog.PostDate;

            DbContext.Blog.Update(updateBlog);

            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("Blog/Delete/{Id}")]
        [HttpGet]
        [RequireAuthentication]
        public async Task<IActionResult> Delete(int Id)
        {
            Blog deleteBlog = DbContext.Blog.Where(article => article.Id == Id).FirstOrDefault();

            DbContext.Blog.Remove(deleteBlog);

            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
