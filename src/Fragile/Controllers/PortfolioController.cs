using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Fragile.Models;
using Fragile.Services;

namespace Fragile.Controllers
{
    public class PortfolioController : BasicController
    {
        public PortfolioController(ApplicationDbContext dbContext, AuthenticationService authenticationService) : 
            base(dbContext, authenticationService) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
