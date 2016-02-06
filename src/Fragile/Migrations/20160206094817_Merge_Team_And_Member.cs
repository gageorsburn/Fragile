using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Fragile.Migrations
{
    public partial class Merge_Team_And_Member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TeamMember",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "PasswordHashData",
                table: "TeamMember",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "SessionKey",
                table: "TeamMember",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Email", table: "TeamMember");
            migrationBuilder.DropColumn(name: "PasswordHashData", table: "TeamMember");
            migrationBuilder.DropColumn(name: "SessionKey", table: "TeamMember");
        }
    }
}
