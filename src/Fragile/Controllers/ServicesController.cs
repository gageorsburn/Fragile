using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Fragile.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Fragile.Controllers
{
    public class ServicesController : Controller
    {
        public ApplicationDbContext dbContext
        {
            get;
        }

        public ServicesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(dbContext.Service);
        }
    }
}
