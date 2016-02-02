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
        // GET: /<controller>/
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

            return View(teamMember);
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
            updateTeamMember.LinkedinUrl = updateTeamMember.LinkedinUrl;

            dbContext.TeamMember.Update(updateTeamMember);

            await dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
            //return View(teamMember);
        }

        [RequireAuthentication]
        public IActionResult Delete()
        {
            return View();
        }
    }
}
