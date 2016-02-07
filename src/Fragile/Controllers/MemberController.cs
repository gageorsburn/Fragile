using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Fragile.Models;
using Fragile.Services;
using Fragile.ViewModels;

using SendGrid;

namespace Fragile.Controllers
{
    public class MemberController : BasicController
    {
        public MemberController(ApplicationDbContext dbContext, AuthenticationService authenticationService, EmailService emailService) : 
            base(dbContext, authenticationService, emailService) { }

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
                var member = DbContext.TeamMember.Where(m => m.Email == signInModel.Email).FirstOrDefault();

                if (member != null)
                {
                    if (member.PasswordHash != null)
                    {
                        if (member.PasswordHash.CompareTo(signInModel.Password))
                        {
                            AuthenticationService.AuthorizedMember = member;
                            return Redirect("/");
                        }
                        else
                        {
                            ModelState.AddModelError("Password", "Invalid password.");
                        }
                    }
                    else
                    {
                        member.ResetPasswordToken = AuthenticationService.Rng.GetHexString(32);
                        EmailService.SendResetPasswordToken(member.Email, member.ResetPasswordToken);

                        DbContext.TeamMember.Update(member);
                        DbContext.SaveChanges();

                        return RedirectToAction("SignIn");
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
            AuthenticationService.AuthorizedMember = null;

            return RedirectToAction("SignIn");
        }
        
        [HttpGet]
        [Route("/Member/ChangePassword/{Email}/{ResetPasswordToken}")]
        public IActionResult ChangePassword(string Email, string ResetPasswordToken)
        {
            return View(new SignInModel { Email = Email, ResetPasswordToken = ResetPasswordToken });
        }

        [HttpPost]
        [Route("/Member/ChangePassword/{Email}/{ResetPasswordToken}")]
        public async Task<IActionResult> ChangePassword(SignInModel signInModel, string ResetPasswordToken)
        {
            var member = DbContext.TeamMember.Where(m => m.Email == signInModel.Email && m.ResetPasswordToken == ResetPasswordToken).FirstOrDefault();

            if(member.PasswordHash == null || AuthenticationService.AuthorizedMember?.Email == member.Email)
            {
                member.PasswordHash = PasswordHashModel.Generate(signInModel.Password);
                member.ResetPasswordToken = null;

                DbContext.TeamMember.Update(member);
                await DbContext.SaveChangesAsync();
            }

            return RedirectToAction("SignIn");
        }

        public IActionResult HashPassword(string Password)
        {
            return Json(PasswordHashModel.Generate(Password));
        }
    }
}
