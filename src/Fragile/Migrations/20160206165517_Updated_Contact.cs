using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Fragile.Migrations
{
    public partial class Updated_Contact : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Contact",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            migrationBuilder.AddColumn<bool>(
                name: "Read",
                table: "Contact",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Date", table: "Contact");
            migrationBuilder.DropColumn(name: "Read", table: "Contact");
        }
    }
}
