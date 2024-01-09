using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class ChangeGroupSubjectRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubjects_Groups_GroupId",
                table: "GroupSubjects");

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "GroupSubjects",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "GroupSubjects",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSubjects_GroupId",
                table: "GroupSubjects",
                newName: "IX_GroupSubjects_ScheduleId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClassId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClassId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClassId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClassId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClassId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "ClassId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7,
                column: "ClassId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClassId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 9,
                column: "ClassId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10,
                column: "ClassId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 12, "Ôn thi đại học" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 11, "Khối A1" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 12, "Lớp 12A4" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 12, "Chuẩn bị thi ĐH" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 12, "AE 12A1" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 10, "10A4" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubjects_Schedules_ScheduleId",
                table: "GroupSubjects",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupSubjects_Schedules_ScheduleId",
                table: "GroupSubjects");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "GroupSubjects",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_GroupSubjects_ScheduleId",
                table: "GroupSubjects",
                newName: "IX_GroupSubjects_GroupId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ClassId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 8,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 9,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10,
                column: "ClassId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 3,
                column: "IsActive",
                value: false);

            migrationBuilder.InsertData(
                table: "GroupSubjects",
                columns: new[] { "Id", "GroupId", "SubjectId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 4 },
                    { 3, 1, 8 },
                    { 4, 2, 1 },
                    { 5, 2, 2 },
                    { 6, 2, 3 },
                    { 7, 3, 5 },
                    { 8, 3, 6 },
                    { 9, 4, 5 },
                    { 10, 5, 5 },
                    { 11, 6, 5 }
                });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 7, "Nhóm 1 của học sinh 1" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 7, "Nhóm 2 của học sinh 1" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 8, "Nhóm 3 của học sinh 2" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 8, "Nhóm 4 của học sinh 2" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 8, "Nhóm 5 của học sinh 3" });

            migrationBuilder.UpdateData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "ClassId", "Name" },
                values: new object[] { 8, "Nhóm 6 của học sinh 3" });

            migrationBuilder.AddForeignKey(
                name: "FK_GroupSubjects_Groups_GroupId",
                table: "GroupSubjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
