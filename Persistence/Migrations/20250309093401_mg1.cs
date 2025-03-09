using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mg1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ExampleEntities",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Code = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OrderNo = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleEntities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OrderNo = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OrderNo = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OrderNo = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    AuthenticatorType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OrderNo = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RevokedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonRevoked = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    OrderNo = table.Column<short>(type: "smallint", nullable: false, defaultValue: (short)1),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "dbo",
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Roles",
                columns: new[] { "Id", "DeletedDate", "Name", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, "Admin", null },
                    { 2, null, "Auth.Admin", null },
                    { 3, null, "Auth.Read", null },
                    { 4, null, "Auth.Write", null },
                    { 5, null, "Auth.RevokeToken", null },
                    { 6, null, "Roles.Admin", null },
                    { 7, null, "Roles.Read", null },
                    { 8, null, "Roles.Write", null },
                    { 9, null, "Roles.Create", null },
                    { 10, null, "Roles.Update", null },
                    { 11, null, "Roles.Delete", null },
                    { 12, null, "UserRoles.Admin", null },
                    { 13, null, "UserRoles.Read", null },
                    { 14, null, "UserRoles.Write", null },
                    { 15, null, "UserRoles.Create", null },
                    { 16, null, "UserRoles.Update", null },
                    { 17, null, "UserRoles.Delete", null },
                    { 18, null, "Users.Admin", null },
                    { 19, null, "Users.Read", null },
                    { 20, null, "Users.Write", null },
                    { 21, null, "Users.Create", null },
                    { 22, null, "Users.Update", null },
                    { 23, null, "Users.Delete", null }
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ExampleEntities_IsDelete",
                schema: "dbo",
                table: "ExampleEntities",
                column: "IsDelete",
                filter: "IsDelete = 0");

            migrationBuilder.CreateIndex(
                name: "UK_ExampleEntities_Name",
                schema: "dbo",
                table: "ExampleEntities",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_IsDelete",
                schema: "dbo",
                table: "Products",
                column: "IsDelete",
                filter: "IsDelete = 0");

            migrationBuilder.CreateIndex(
                name: "UK_Products_Name",
                schema: "dbo",
                table: "Products",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                schema: "dbo",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "dbo",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                schema: "dbo",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExampleEntities",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Products",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "RefreshTokens",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");
        }
    }
}
