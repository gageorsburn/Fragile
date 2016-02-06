using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Fragile.Migrations
{
    public partial class Changed_Password_Token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ResetPasswordToken",
                table: "TeamMember",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "IPAddress",
                table: "Contact",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "IPAddress", table: "Contact");
            migrationBuilder.AlterColumn<Guid>(
                name: "ResetPasswordToken",
                table: "TeamMember",
                nullable: false);
        }
    }
}
