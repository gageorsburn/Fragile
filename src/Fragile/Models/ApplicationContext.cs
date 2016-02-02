using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fragile.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Service> Service { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<CompanyEvent> CompanyEvent { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }

    }
}
