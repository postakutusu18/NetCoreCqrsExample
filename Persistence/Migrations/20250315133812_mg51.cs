using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg51 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "ExampleEntities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("08dd63c6-a166-eac7-80fa-5b98d5810000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "DeletedDate", "Email", "FirstName", "IsActive", "IsDelete", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("01d1c549-239d-4c6f-8088-0f55a66dd6a9"), 1, null, "postakutusu18@hotmail.com", "Selçuk", true, false, "KARAAĞAÇ", new byte[] { 130, 26, 100, 129, 32, 213, 212, 77, 120, 253, 157, 137, 127, 17, 246, 138, 57, 61, 84, 58, 217, 98, 238, 99, 50, 176, 246, 182, 135, 69, 92, 236, 208, 95, 63, 141, 246, 56, 244, 178, 20, 77, 1, 70, 100, 128, 3, 250, 203, 77, 130, 176, 90, 103, 203, 127, 189, 96, 214, 17, 79, 81, 148, 152 }, new byte[] { 37, 96, 8, 174, 112, 62, 208, 216, 46, 143, 247, 39, 141, 111, 62, 137, 96, 15, 65, 146, 9, 169, 126, 49, 70, 206, 1, 175, 219, 43, 209, 168, 144, 167, 171, 210, 35, 44, 42, 191, 187, 62, 19, 94, 119, 141, 214, 126, 93, 190, 110, 91, 223, 88, 14, 249, 175, 67, 24, 116, 113, 197, 105, 24, 192, 197, 149, 149, 107, 10, 111, 42, 125, 182, 69, 94, 193, 206, 21, 109, 151, 98, 1, 251, 100, 135, 174, 55, 93, 99, 25, 127, 37, 71, 38, 17, 198, 149, 90, 185, 200, 196, 225, 45, 19, 30, 188, 205, 36, 63, 241, 28, 142, 96, 16, 35, 154, 239, 143, 234, 52, 171, 130, 47, 181, 28, 52, 24 }, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRoles",
                columns: new[] { "Id", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("3893330e-8681-45fe-90c5-c4c66ca08364"), null, 1, null, new Guid("01d1c549-239d-4c6f-8088-0f55a66dd6a9") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("3893330e-8681-45fe-90c5-c4c66ca08364"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("01d1c549-239d-4c6f-8088-0f55a66dd6a9"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "ExampleEntities",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("08dd63c6-a166-eac7-80fa-5b98d5810000"));

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
    }
}
