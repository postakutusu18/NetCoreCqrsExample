using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { new Guid("f1666f92-70f5-43ba-a744-8bed67540ffe"), 1, null, "postakutusu18@hotmail.com", "Selçuk", true, false, "KARAAĞAÇ", new byte[] { 75, 179, 36, 116, 83, 73, 226, 166, 80, 164, 242, 63, 55, 171, 39, 35, 221, 154, 48, 13, 207, 204, 141, 225, 26, 207, 177, 234, 142, 161, 193, 49, 215, 89, 252, 123, 48, 103, 20, 224, 116, 113, 148, 64, 242, 1, 51, 48, 169, 239, 94, 140, 78, 99, 245, 187, 212, 240, 215, 2, 235, 252, 43, 246 }, new byte[] { 176, 152, 92, 69, 64, 9, 23, 215, 191, 53, 40, 198, 116, 247, 9, 40, 104, 89, 164, 151, 126, 211, 98, 195, 143, 88, 253, 18, 252, 116, 252, 148, 236, 148, 41, 218, 200, 4, 38, 200, 158, 56, 192, 113, 31, 191, 206, 211, 137, 22, 73, 139, 84, 49, 227, 164, 168, 215, 31, 218, 117, 20, 227, 164, 167, 50, 46, 58, 110, 130, 110, 41, 197, 67, 238, 117, 156, 142, 42, 82, 161, 85, 38, 40, 199, 234, 156, 18, 223, 81, 36, 67, 145, 212, 2, 6, 55, 198, 63, 128, 244, 184, 28, 87, 255, 59, 86, 184, 193, 235, 55, 40, 155, 187, 248, 89, 156, 71, 171, 22, 179, 74, 71, 28, 157, 203, 84, 237 }, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRoles",
                columns: new[] { "Id", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("2e9d7322-ac43-4876-a436-49cc445cde86"), null, 1, null, new Guid("f1666f92-70f5-43ba-a744-8bed67540ffe") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("2e9d7322-ac43-4876-a436-49cc445cde86"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f1666f92-70f5-43ba-a744-8bed67540ffe"));

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
        }
    }
}
