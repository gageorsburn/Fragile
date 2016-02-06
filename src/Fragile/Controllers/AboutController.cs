using Microsoft.AspNet.Mvc;

using Fragile.Models;
using Fragile.Services;

namespace Fragile.Controllers
{
    public class AboutController : BasicController
    {
        public AboutController(ApplicationDbContext dbContext, AuthenticationService authenticationService) : 
            base(dbContext, authenticationService) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
