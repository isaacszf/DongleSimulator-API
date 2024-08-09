using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DongleSimulator.Infra.TableMigrations
{
    /// <inheritdoc />
    public partial class AddAdminUserSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "schema_dongle_simulator",
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "Role", "UserIdentifier" },
                values: new object[] { 1L, new DateTime(2024, 8, 9, 3, 55, 7, 231, DateTimeKind.Utc).AddTicks(4192), "dglsim@email.com", "dongle_admin", "f5ed6428e4f6316d18b3c8dd27320d35f402084c18222575e36d5a1b7beeb9c2b1fdc878f66bcd4757e70973212281d94be330bad505524543cf30ff46fb4f8b", "admin", new Guid("25e0cbe2-b164-43ee-a27a-0fc79520f9ed") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "schema_dongle_simulator",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
