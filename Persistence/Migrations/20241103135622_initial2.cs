using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("c9aa04e4-eb9a-4ccf-b8f2-6c2e074d18f1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b0346143-5651-4a57-90a2-8751a000770a"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "FirstName", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("48cf371d-59af-4f9c-bbd9-0eddc048c345"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "postakutusu18@hotmail.com", "", "", new byte[] { 0, 180, 149, 5, 137, 241, 200, 231, 233, 16, 201, 225, 4, 169, 91, 250, 239, 207, 45, 200, 20, 191, 143, 48, 246, 227, 51, 247, 252, 138, 208, 135, 96, 225, 104, 250, 160, 139, 124, 141, 83, 234, 64, 18, 178, 146, 104, 232, 166, 80, 157, 246, 44, 116, 154, 186, 164, 216, 145, 56, 83, 179, 156, 8 }, new byte[] { 19, 251, 112, 16, 85, 118, 153, 208, 53, 140, 167, 154, 173, 28, 82, 149, 94, 47, 249, 166, 109, 83, 156, 243, 255, 173, 202, 102, 152, 40, 93, 220, 212, 167, 92, 166, 164, 192, 169, 189, 150, 215, 224, 144, 179, 96, 154, 178, 129, 163, 119, 52, 212, 201, 31, 130, 69, 42, 119, 107, 87, 218, 213, 0, 143, 91, 63, 14, 28, 185, 69, 210, 10, 153, 91, 98, 90, 155, 227, 184, 48, 232, 8, 134, 164, 63, 183, 89, 192, 207, 35, 4, 188, 175, 79, 82, 84, 77, 12, 170, 110, 226, 18, 52, 52, 41, 93, 110, 41, 202, 243, 206, 110, 91, 79, 16, 201, 245, 219, 206, 29, 124, 11, 37, 239, 47, 65, 4 }, null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("18c2f8ac-8250-4030-8948-245e9c300e13"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("48cf371d-59af-4f9c-bbd9-0eddc048c345") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("18c2f8ac-8250-4030-8948-245e9c300e13"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("48cf371d-59af-4f9c-bbd9-0eddc048c345"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "CreatedDate", "DeletedDate", "Email", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("b0346143-5651-4a57-90a2-8751a000770a"), 0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "postakutusu18@hotmail.com", new byte[] { 75, 18, 132, 58, 251, 2, 236, 39, 155, 198, 198, 62, 1, 11, 118, 119, 25, 160, 203, 221, 17, 214, 81, 158, 108, 54, 149, 143, 215, 57, 189, 247, 160, 108, 187, 29, 217, 150, 214, 50, 125, 107, 162, 199, 203, 7, 61, 153, 46, 29, 175, 152, 16, 216, 131, 159, 238, 134, 50, 57, 84, 99, 60, 118 }, new byte[] { 195, 111, 135, 252, 167, 79, 187, 164, 252, 76, 61, 27, 182, 146, 210, 198, 33, 24, 149, 103, 243, 101, 30, 165, 55, 22, 40, 207, 179, 187, 131, 17, 36, 41, 192, 119, 90, 33, 192, 234, 158, 36, 150, 164, 103, 233, 99, 58, 117, 255, 145, 246, 248, 153, 62, 225, 101, 20, 72, 195, 233, 120, 80, 232, 48, 151, 102, 148, 248, 80, 98, 243, 108, 240, 83, 120, 133, 136, 65, 134, 50, 113, 144, 78, 193, 188, 227, 149, 45, 55, 104, 1, 114, 99, 139, 172, 125, 95, 178, 13, 122, 121, 47, 104, 154, 104, 223, 168, 220, 81, 6, 145, 128, 246, 42, 16, 224, 163, 144, 134, 100, 170, 208, 14, 115, 155, 32, 86 }, null });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedDate", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("c9aa04e4-eb9a-4ccf-b8f2-6c2e074d18f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, new Guid("b0346143-5651-4a57-90a2-8751a000770a") });
        }
    }
}
