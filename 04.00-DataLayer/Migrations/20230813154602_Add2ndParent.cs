using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class Add2ndParent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "ClassId", "Dob", "Email", "FullName", "Password", "Phone", "RoleId", "Schhool", "Username" },
                values: new object[] { 12, null, new DateTime(1975, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "parent2@gmail.com", "Parent 2", "15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225", "0123456789", 1, null, "parent2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
