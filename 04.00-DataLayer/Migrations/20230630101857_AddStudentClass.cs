using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class AddStudentClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ClassId",
                table: "Accounts",
                column: "ClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Classes_ClassId",
                table: "Accounts",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Classes_ClassId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ClassId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "Accounts");
        }
    }
}
