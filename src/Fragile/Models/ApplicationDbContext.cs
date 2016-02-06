using Microsoft.Data.Entity;

namespace Fragile.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() : base() { }

        public DbSet<Service> Service { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<CompanyEvent> CompanyEvent { get; set; }
        public DbSet<TeamMember> TeamMember { get; set; }
        public DbSet<Blog> Blog { get; set; }
    }
}
