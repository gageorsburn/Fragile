using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Fragile.Attributes;
using Fragile.Models;
using Fragile.Services;

namespace Fragile.Controllers
{
    public class TeamController : BasicController
    {
        public TeamController(ApplicationDbContext dbContext, AuthenticationService authenticationService, EmailService emailService) : 
            base(dbContext, authenticationService, emailService) { }

        public IActionResult Index()
        {
            return View(DbContext.TeamMember);
        }
        
        [HttpGet]
        [RequireAuthentication]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [RequireAuthentication]
        public async Task<IActionResult> Create(TeamMember teamMember)
        {
            teamMember.ResetPasswordToken = AuthenticationService.Rng.GetBase64String(32);

            EmailService.SendResetPasswordToken(teamMember.Email, teamMember.ResetPasswordToken);

            DbContext.TeamMember.Add(teamMember);
            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("Team/Update/{Name}")]
        [HttpGet]
        [RequireAuthentication]
        public IActionResult Update(string Name)
        {
            return View(DbContext.TeamMember.Where(m => m.Name == Name).FirstOrDefault());
        }

        [Route("Team/Update/{Name}")]
        [HttpPost]
        [RequireAuthentication]
        public async Task<IActionResult> Update(string Name, TeamMember teamMember)
        {
            TeamMember updateTeamMember = DbContext.TeamMember.Where(m => m.Name == Name).FirstOrDefault();

            updateTeamMember.Email = teamMember.Email;
            updateTeamMember.Role = teamMember.Role;
            updateTeamMember.ProfileImageUrl = teamMember.ProfileImageUrl;
            updateTeamMember.FacebookUrl = teamMember.FacebookUrl;
            updateTeamMember.TwitterUrl = teamMember.TwitterUrl;
            updateTeamMember.LinkedinUrl = teamMember.LinkedinUrl;
            updateTeamMember.GitHubUrl = teamMember.GitHubUrl;

            DbContext.TeamMember.Update(updateTeamMember);

            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("Team/Delete/{Name}")]
        [HttpGet]
        [RequireAuthentication]
        public async Task<IActionResult> Delete(string Name)
        {
            TeamMember deleteTeamMember = DbContext.TeamMember.Where(m => m.Name == Name).FirstOrDefault();

            DbContext.TeamMember.Remove(deleteTeamMember);

            await DbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
