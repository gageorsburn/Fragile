﻿using Microsoft.AspNet.Mvc;

using Fragile.Models;
using Fragile.Services;
using Fragile.Attributes;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNet.Http.Features;

namespace Fragile.Controllers
{
    public class ContactController : BasicController
    {
        public ContactController(ApplicationDbContext dbContext, AuthenticationService authenticationService, EmailService emailService) : 
            base(dbContext, authenticationService, emailService) { }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            contact.Date = DateTime.Now;
            contact.IPAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString();

            DbContext.Contact.Add(contact);
            await DbContext.SaveChangesAsync();

            EmailService.SendContactNotification(contact.Email, contact.Message);

            return RedirectToAction("Index");
        }

        [RequireAuthentication]
        public IActionResult ViewMessages()
        {
            return View(DbContext.Contact);
        }

        [Route("Contact/ViewMessages/{Id}")]
        [RequireAuthentication]
        public async Task<IActionResult> ViewMessage(int Id)
        {
            var message = DbContext.Contact.Where(m => m.Id == Id).FirstOrDefault();

            message.Read = true;

            DbContext.Contact.Update(message);
            await DbContext.SaveChangesAsync();

            return View(message);
        }
    }
}
