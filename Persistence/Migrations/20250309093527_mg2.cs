using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("f46cda44-f7c0-4a63-8183-02c94a90ef2c"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("412d1e96-3ddb-4f42-844b-ca0affe5e1ea"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[] { new Guid("412d1e96-3ddb-4f42-844b-ca0affe5e1ea"), 1, null, "postakutusu18@hotmail.com", "Selçuk", true, false, "KARAAĞAÇ", new byte[] { 118, 204, 64, 68, 212, 238, 239, 241, 222, 216, 190, 36, 142, 28, 13, 219, 37, 13, 213, 79, 196, 227, 118, 137, 153, 125, 199, 232, 156, 69, 182, 122, 255, 192, 98, 37, 213, 118, 210, 46, 23, 70, 135, 76, 68, 72, 99, 216, 202, 72, 20, 139, 174, 46, 228, 244, 166, 206, 159, 178, 208, 164, 43, 163 }, new byte[] { 0, 51, 225, 113, 205, 160, 242, 0, 44, 217, 146, 111, 221, 255, 168, 169, 18, 23, 179, 2, 95, 177, 36, 241, 199, 40, 178, 182, 215, 187, 147, 244, 145, 150, 50, 133, 145, 175, 96, 197, 129, 246, 150, 4, 255, 59, 222, 98, 68, 57, 160, 43, 52, 32, 27, 67, 138, 227, 217, 64, 240, 1, 200, 199, 71, 22, 198, 72, 107, 113, 16, 180, 157, 241, 64, 86, 195, 204, 109, 197, 125, 154, 33, 87, 229, 155, 117, 14, 37, 179, 130, 70, 184, 52, 211, 75, 141, 192, 252, 252, 109, 73, 21, 89, 207, 8, 89, 159, 74, 219, 64, 38, 207, 196, 121, 38, 200, 119, 63, 8, 12, 129, 255, 66, 224, 218, 157, 60 }, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRoles",
                columns: new[] { "Id", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("f46cda44-f7c0-4a63-8183-02c94a90ef2c"), null, 1, null, new Guid("412d1e96-3ddb-4f42-844b-ca0affe5e1ea") });
        }
    }
}
