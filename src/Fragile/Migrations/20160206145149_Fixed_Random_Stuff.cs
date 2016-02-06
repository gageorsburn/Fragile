using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Fragile.Migrations
{
    public partial class Fixed_Random_Stuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "DatePosted", table: "Blog");
            migrationBuilder.DropColumn(name: "PostAfterDate", table: "Blog");
            migrationBuilder.AddColumn<string>(
                name: "GitHubUrl",
                table: "TeamMember",
                nullable: true);
            migrationBuilder.AddColumn<Guid>(
                name: "ResetPasswordToken",
                table: "TeamMember",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Blog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "GitHubUrl", table: "TeamMember");
            migrationBuilder.DropColumn(name: "ResetPasswordToken", table: "TeamMember");
            migrationBuilder.DropColumn(name: "PostDate", table: "Blog");
            migrationBuilder.AddColumn<DateTime>(
                name: "DatePosted",
                table: "Blog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            migrationBuilder.AddColumn<DateTime>(
                name: "PostAfterDate",
                table: "Blog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
