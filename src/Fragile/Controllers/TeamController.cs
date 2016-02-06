using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Fragile.Attributes;
using Fragile.Models;

namespace Fragile.Controllers
{
    //[Route("Team")]
    public class TeamController : Controller
    {
        public ApplicationDbContext dbContext
        {
            get;
        }

        public TeamController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(dbContext.TeamMember);
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
            dbContext.TeamMember.Add(teamMember);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("Team/Update/{Name}")]
        [HttpGet]
        [RequireAuthentication]
        public IActionResult Update(string Name)
        {
            return View(dbContext.TeamMember.Where(m => m.Name == Name).FirstOrDefault());
        }

        [Route("Team/Update/{Name}")]
        [HttpPost]
        [RequireAuthentication]
        public async Task<IActionResult> Update(string Name, TeamMember teamMember)
        {
            TeamMember updateTeamMember = dbContext.TeamMember.Where(m => m.Name == Name).FirstOrDefault();

            updateTeamMember.Role = teamMember.Role;
            updateTeamMember.ProfileImageUrl = teamMember.ProfileImageUrl;
            updateTeamMember.FacebookUrl = teamMember.FacebookUrl;
            updateTeamMember.TwitterUrl = teamMember.TwitterUrl;
            updateTeamMember.LinkedinUrl = teamMember.LinkedinUrl;
            updateTeamMember.Email = teamMember.Email;

            dbContext.TeamMember.Update(updateTeamMember);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [Route("Team/Delete/{Name}")]
        [HttpGet]
        [RequireAuthentication]
        public async Task<IActionResult> Delete(string Name)
        {
            TeamMember deleteTeamMember = dbContext.TeamMember.Where(m => m.Name == Name).FirstOrDefault();

            dbContext.TeamMember.Remove(deleteTeamMember);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
