using Microsoft.AspNet.Mvc;

using Fragile.Services;
using Fragile.Models;

namespace Fragile.Controllers
{
    public class BasicController : Controller
    {
        public ApplicationDbContext DbContext
        {
            get;
        }

        public AuthenticationService AuthenticationService
        {
            get;
        }

        public EmailService EmailService
        {
            get;
        }

        public BasicController(ApplicationDbContext dbContext,
            AuthenticationService authenticationService)
        {
            this.DbContext = dbContext;
            this.AuthenticationService = authenticationService;
        }

        public BasicController(ApplicationDbContext dbContext, 
            AuthenticationService authenticationService, EmailService emailService)
        {
            this.DbContext = dbContext;
            this.AuthenticationService = authenticationService;
            this.EmailService = emailService;
        }
    }
}
