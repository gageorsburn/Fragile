﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Fragile.Models;
using Fragile.Services;
using Fragile.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Fragile.Controllers
{
    public class MemberController : Controller
    {
        public ApplicationDbContext DbContext
        {
            get;
        }

        public AuthenticationService Authentication
        {
            get;
        }

        public MemberController(ApplicationDbContext dbContext, AuthenticationService authenticationService)
        {
            DbContext = dbContext;
            Authentication = authenticationService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var member = DbContext.Member.Where(m => m.Email == signInModel.Email).FirstOrDefault();

                if (member != null)
                {
                    if (member.PasswordHash.CompareTo(signInModel.Password))
                    {
                        Authentication.AuthorizedMember = member;
                        return Redirect("/");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Invalid password.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Invalid password.");
                }
            }

            return View(signInModel);
        }

        public IActionResult SignOut()
        {
            Authentication.AuthorizedMember = null;

            return View();
        }

        public IActionResult HashPassword(string Password)
        {
            return Json(PasswordHashModel.Generate(Password));
        }
    }
}