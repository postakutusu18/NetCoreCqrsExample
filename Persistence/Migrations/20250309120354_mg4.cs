using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("d3d18826-dfa4-41ad-8394-d04d2e7548d3"), 1, null, "postakutusu18@hotmail.com", "Selçuk", true, false, "KARAAĞAÇ", new byte[] { 99, 242, 161, 64, 112, 91, 43, 183, 0, 104, 224, 75, 188, 42, 171, 170, 205, 3, 22, 167, 149, 194, 9, 86, 48, 25, 235, 254, 80, 6, 204, 20, 219, 233, 108, 4, 82, 55, 87, 156, 205, 131, 104, 84, 53, 218, 91, 227, 172, 113, 229, 52, 224, 83, 191, 116, 194, 169, 171, 183, 209, 186, 61, 253 }, new byte[] { 25, 82, 185, 141, 221, 85, 170, 250, 200, 0, 86, 3, 224, 26, 210, 134, 171, 231, 161, 46, 139, 235, 167, 72, 188, 89, 50, 78, 136, 95, 247, 214, 175, 254, 158, 244, 50, 77, 90, 147, 119, 133, 31, 140, 52, 76, 246, 219, 117, 231, 127, 93, 211, 3, 240, 201, 160, 220, 120, 62, 134, 119, 207, 210, 162, 97, 253, 87, 7, 250, 213, 92, 8, 136, 116, 9, 236, 211, 233, 240, 208, 45, 30, 193, 79, 97, 209, 180, 75, 197, 171, 29, 30, 135, 95, 189, 100, 23, 49, 69, 106, 220, 134, 136, 113, 78, 101, 58, 237, 161, 9, 188, 76, 50, 101, 76, 29, 23, 98, 42, 161, 212, 106, 251, 73, 125, 227, 52 }, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRoles",
                columns: new[] { "Id", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("590fd72a-724c-4221-ae2c-3f6788b60f94"), null, 1, null, new Guid("d3d18826-dfa4-41ad-8394-d04d2e7548d3") });

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_IsDelete",
                schema: "dbo",
                table: "UserRoles",
                column: "IsDelete",
                filter: "IsDelete = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_IsDelete",
                schema: "dbo",
                table: "Roles",
                column: "IsDelete",
                filter: "IsDelete = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_IsDelete",
                schema: "dbo",
                table: "RefreshTokens",
                column: "IsDelete",
                filter: "IsDelete = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRoles_IsDelete",
                schema: "dbo",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_IsDelete",
                schema: "dbo",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_IsDelete",
                schema: "dbo",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("590fd72a-724c-4221-ae2c-3f6788b60f94"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d3d18826-dfa4-41ad-8394-d04d2e7548d3"));

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
        }
    }
}
