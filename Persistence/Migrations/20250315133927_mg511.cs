using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg511 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                defaultValue: new Guid("08dd63c6-ce57-f4bf-80fa-5b98d5810000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("08dd63c6-a166-eac7-80fa-5b98d5810000"));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "AuthenticatorType", "DeletedDate", "Email", "FirstName", "IsActive", "IsDelete", "LastName", "PasswordHash", "PasswordSalt", "UpdatedDate" },
                values: new object[] { new Guid("2ab2fcef-702f-4002-ae3c-913e2157b998"), 1, null, "postakutusu18@hotmail.com", "Selçuk", true, false, "KARAAĞAÇ", new byte[] { 34, 9, 77, 66, 38, 140, 63, 74, 209, 9, 96, 78, 210, 199, 169, 12, 24, 239, 183, 153, 188, 210, 72, 192, 145, 229, 246, 117, 255, 209, 201, 214, 143, 56, 105, 186, 75, 51, 53, 186, 0, 83, 165, 138, 128, 248, 50, 43, 59, 6, 98, 36, 95, 52, 87, 22, 114, 197, 244, 64, 176, 206, 193, 12 }, new byte[] { 86, 87, 110, 228, 253, 177, 228, 232, 56, 39, 47, 19, 148, 193, 144, 64, 68, 12, 78, 69, 41, 121, 110, 100, 48, 7, 35, 114, 85, 71, 124, 66, 236, 158, 167, 109, 158, 219, 177, 17, 108, 121, 70, 121, 198, 141, 236, 117, 165, 108, 128, 56, 78, 153, 104, 176, 251, 47, 222, 20, 58, 137, 83, 29, 82, 154, 124, 152, 120, 152, 28, 38, 2, 171, 162, 158, 55, 201, 187, 56, 28, 105, 59, 102, 103, 217, 6, 73, 49, 58, 96, 175, 71, 197, 221, 117, 236, 252, 14, 214, 224, 169, 235, 66, 144, 228, 71, 253, 197, 236, 101, 219, 63, 135, 228, 194, 148, 180, 84, 180, 74, 167, 75, 246, 233, 202, 53, 186 }, null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "UserRoles",
                columns: new[] { "Id", "DeletedDate", "RoleId", "UpdatedDate", "UserId" },
                values: new object[] { new Guid("4f229c77-ed4b-4805-bbc5-1d979b3b65ad"), null, 1, null, new Guid("2ab2fcef-702f-4002-ae3c-913e2157b998") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("4f229c77-ed4b-4805-bbc5-1d979b3b65ad"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2ab2fcef-702f-4002-ae3c-913e2157b998"));

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                schema: "dbo",
                table: "ExampleEntities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("08dd63c6-a166-eac7-80fa-5b98d5810000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("08dd63c6-ce57-f4bf-80fa-5b98d5810000"));

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
    }
}
