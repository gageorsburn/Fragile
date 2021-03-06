using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using Fragile.Models;

namespace Fragile.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160202081051_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

                    b.Property<string>("Email");

                    b.Property<string>("Message");

                    b.Property<string>("Name");

                    b.Property<string>("PhoneNumber");

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

                    b.Property<string>("FacebookUrl");

                    b.Property<string>("LinkedinUrl");

                    b.Property<string>("ProfileImageUrl");

                    b.Property<string>("Role");

                    b.Property<string>("TwitterUrl");

                    b.HasKey("Name");
                });
        }
    }
}
