using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Fragile.Models;

namespace Fragile.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160206165517_Updated_Contact")]
    partial class Updated_Contact
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fragile.Models.Blog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AuthorName");

                    b.Property<string>("Body");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Fragile.Models.CompanyEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Title");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Fragile.Models.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Email");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("Read");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Fragile.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<string>("ClientName");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.Property<string>("ShortDescription");

                    b.HasKey("Id");
                });

            modelBuilder.Entity("Fragile.Models.Service", b =>
                {
                    b.Property<int>("ServiceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Heading");

                    b.Property<string>("ImageName");

                    b.HasKey("ServiceID");
                });

            modelBuilder.Entity("Fragile.Models.TeamMember", b =>
                {
                    b.Property<string>("Name");

                    b.Property<string>("Email");

                    b.Property<string>("FacebookUrl");

                    b.Property<string>("GitHubUrl");

                    b.Property<string>("LinkedinUrl");

                    b.Property<string>("PasswordHashData");

                    b.Property<string>("ProfileImageUrl");

                    b.Property<Guid>("ResetPasswordToken");

                    b.Property<string>("Role");

                    b.Property<string>("SessionKey");

                    b.Property<string>("TwitterUrl");

                    b.HasKey("Name");
                });
        }
    }
}
