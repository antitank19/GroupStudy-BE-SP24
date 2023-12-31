using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 6, 6 },
                    { 7, 7 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 10 },
                    { 11, 11 },
                    { 12, 12 }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Parent" },
                    { 2, "Student" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Toán" },
                    { 2, "Lí" },
                    { 3, "Hóa" },
                    { 4, "Văn" },
                    { 5, "Sử" },
                    { 6, "Địa" },
                    { 7, "Sinh" },
                    { 8, "Anh" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Dob", "Email", "FullName", "Password", "Phone", "RoleId", "Username" },
                values: new object[,]
                {
                    { 1, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "trankhaiminhkhoi10a3@gmail.com", "Tran Khai Minh Khoi", "123456789", "0123456789", 2, "student1" },
                    { 2, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "student2@gmail.com", "Dao Thi B", "123456789", "0123456789", 2, "student2" },
                    { 3, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "student3@gmail.com", "Tran Van C", "123456789", "0123456789", 2, "student3" },
                    { 4, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "student4@gmail.com", "Li Thi D", "123456789", "0123456789", 2, "student4" },
                    { 5, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "student5@gmail.com", "Tran Van E", "123456789", "0123456789", 2, "student5" },
                    { 6, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "student6@gmail.com", "Li Chinh F", "123456789", "0123456789", 2, "student6" },
                    { 7, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "student7@gmail.com", "Ngo Van G", "123456789", "0123456789", 2, "student7" },
                    { 8, null, "student8@gmail.com", "Tran Van H", "123456789", "0123456789", 2, "student8" },
                    { 9, null, "student9@gmail.com", "Tran Van I", "123456789", "0123456789", 2, "student9" },
                    { 10, new DateTime(2009, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "student10@gmail.com", "Tran Van J", "123456789", "0123456789", 2, "student10" },
                    { 11, new DateTime(1975, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "trankhaiminhkhoi@gmail.com", "Tran Khoi", "123456789", "0123456789", 1, "parent1" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "ClassId", "Name" },
                values: new object[,]
                {
                    { 1, 7, "Nhóm 1 của học sinh 1" },
                    { 2, 7, "Nhóm 2 của học sinh 1" },
                    { 3, 8, "Nhóm 1 của học sinh 2" },
                    { 4, 8, "Nhóm 2 của học sinh 2" },
                    { 5, 8, "Nhóm 1 của học sinh 3" },
                    { 6, 8, "Nhóm 2 của học sinh 3" }
                });

            migrationBuilder.InsertData(
                table: "GroupMembers",
                columns: new[] { "Id", "AccountId", "GroupId", "State" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 5, 1, 3 },
                    { 4, 1, 2, 1 },
                    { 5, 2, 2, 2 },
                    { 6, 2, 3, 1 },
                    { 7, 1, 3, 2 },
                    { 8, 2, 4, 1 },
                    { 9, 3, 5, 1 },
                    { 10, 3, 6, 1 }
                });

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
                    { 8, 3, 6 }
                });

            migrationBuilder.InsertData(
                table: "JoinInvites",
                columns: new[] { "Id", "AccountId", "GroupId", "State" },
                values: new object[,]
                {
                    { 1, 2, 1, 1 },
                    { 2, 3, 1, 2 },
                    { 3, 2, 2, 1 },
                    { 4, 3, 2, 2 },
                    { 5, 1, 3, 1 },
                    { 6, 3, 3, 2 },
                    { 7, 3, 4, 2 },
                    { 8, 3, 5, 2 },
                    { 9, 2, 6, 2 },
                    { 10, 1, 6, 2 }
                });

            migrationBuilder.InsertData(
                table: "JoinRequests",
                columns: new[] { "Id", "AccountId", "GroupId", "State" },
                values: new object[,]
                {
                    { 1, 4, 1, 2 },
                    { 2, 3, 2, 2 },
                    { 3, 4, 3, 2 },
                    { 4, 4, 4, 2 },
                    { 5, 3, 5, 2 },
                    { 6, 1, 6, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "GroupMembers",
                keyColumn: "Id",
                keyValue: 10);

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
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "JoinInvites",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "JoinRequests",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "JoinRequests",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "JoinRequests",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "JoinRequests",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "JoinRequests",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "JoinRequests",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
