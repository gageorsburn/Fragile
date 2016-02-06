using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Fragile.Models;
using Fragile.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Fragile.Controllers
{
    public class ServicesController : BasicController
    {
        public ServicesController(ApplicationDbContext dbContext, AuthenticationService authenticationService) : 
            base(dbContext, authenticationService) { }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(DbContext.Service);
        }
    }
}
