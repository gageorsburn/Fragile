using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace Fragile.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() : base()
        {

        }

        public DbSet<Service> Service { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<CompanyEvent> CompanyEvent { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
        public DbSet<Member> Member { get; set; }
    }
}
