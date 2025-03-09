using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("5730c6b8-ed95-4e9b-9b6c-c668f87a1d06"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("89669842-566e-4a0d-81ba-8adf8b93130c"));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "DeletedDate", "Email", "FirstName", "IsActive", "IsDelete", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("f9d3aaa6-bacd-444a-94cc-afec047696dc"), 1, null, "postakutusu18@hotmail.com", "Selçuk", true, false, "KARAAĞAÇ", new byte[] { 194, 100, 184, 195, 113, 100, 55, 128, 146, 34, 177, 159, 185, 207, 17, 13, 186, 103, 233, 67, 145, 158, 219, 225, 145, 199, 22, 34, 126, 183, 254, 160, 214, 250, 209, 151, 10, 177, 171, 158, 61, 229, 111, 5, 98, 142, 223, 217, 86, 3, 48, 88, 93, 129, 5, 181, 236, 221, 9, 119, 138, 53, 119, 29 }, new byte[] { 128, 156, 223, 33, 151, 107, 235, 66, 37, 27, 213, 11, 223, 180, 196, 69, 194, 13, 123, 44, 48, 241, 92, 167, 190, 250, 61, 246, 238, 61, 143, 153, 180, 206, 114, 158, 184, 63, 166, 1, 14, 250, 231, 148, 218, 206, 69, 236, 129, 54, 161, 158, 151, 185, 140, 42, 209, 113, 202, 89, 135, 169, 102, 227, 66, 51, 244, 45, 133, 75, 67, 214, 107, 73, 236, 67, 251, 169, 87, 87, 28, 135, 192, 214, 186, 218, 13, 202, 109, 66, 32, 196, 195, 114, 74, 127, 40, 97, 75, 135, 138, 182, 112, 17, 139, 128, 204, 42, 204, 229, 204, 85, 228, 254, 127, 233, 22, 124, 108, 120, 104, 54, 95, 179, 91, 93, 9, 133 }, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRoles",
                columns: new[] { "Id", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("0e72a5bb-f502-4a73-a80a-87ba57d8e650"), null, 1, null, new Guid("f9d3aaa6-bacd-444a-94cc-afec047696dc") });

            migrationBuilder.CreateIndex(
                name: "IX_Users_IsDelete",
                schema: "dbo",
                table: "Users",
                column: "IsDelete",
                filter: "IsDelete = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_IsDelete",
                schema: "dbo",
                table: "Users");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("0e72a5bb-f502-4a73-a80a-87ba57d8e650"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f9d3aaa6-bacd-444a-94cc-afec047696dc"));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "DeletedDate", "Email", "FirstName", "IsActive", "IsDelete", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("89669842-566e-4a0d-81ba-8adf8b93130c"), 1, null, "postakutusu18@hotmail.com", "Selçuk", true, false, "KARAAĞAÇ", new byte[] { 25, 135, 83, 231, 128, 140, 69, 51, 151, 172, 140, 136, 172, 18, 158, 153, 189, 97, 71, 201, 190, 163, 149, 68, 70, 160, 205, 67, 3, 73, 42, 54, 226, 123, 180, 65, 210, 113, 67, 169, 105, 161, 52, 202, 178, 240, 184, 225, 170, 167, 46, 70, 120, 136, 79, 132, 253, 45, 69, 45, 218, 210, 247, 93 }, new byte[] { 238, 8, 147, 179, 201, 77, 42, 172, 73, 38, 132, 20, 187, 242, 233, 232, 228, 246, 18, 204, 228, 40, 91, 208, 191, 209, 64, 64, 132, 141, 205, 255, 34, 67, 103, 53, 53, 141, 245, 218, 88, 247, 233, 183, 128, 243, 61, 8, 73, 68, 189, 161, 50, 147, 46, 63, 108, 53, 202, 43, 249, 206, 230, 221, 180, 214, 37, 125, 245, 117, 63, 12, 223, 187, 45, 55, 80, 100, 90, 27, 148, 15, 222, 95, 247, 58, 117, 86, 107, 217, 90, 243, 191, 195, 231, 46, 12, 4, 229, 64, 167, 219, 59, 209, 254, 218, 43, 217, 218, 180, 31, 49, 99, 99, 175, 164, 33, 70, 187, 166, 254, 128, 120, 214, 169, 124, 55, 48 }, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRoles",
                columns: new[] { "Id", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("5730c6b8-ed95-4e9b-9b6c-c668f87a1d06"), null, 1, null, new Guid("89669842-566e-4a0d-81ba-8adf8b93130c") });
        }
    }
}
