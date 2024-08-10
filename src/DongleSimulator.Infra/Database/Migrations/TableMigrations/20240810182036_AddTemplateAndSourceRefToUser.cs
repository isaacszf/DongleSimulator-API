using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DongleSimulator.Infra.TableMigrations
{
    /// <inheritdoc />
    public partial class AddTemplateAndSourceRefToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "schema_dongle_simulator",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Email", "Password", "UserIdentifier" },
                values: new object[] { new DateTime(2024, 8, 10, 18, 20, 35, 407, DateTimeKind.Utc).AddTicks(9641), "dglsim@email.com", "f5ed6428e4f6316d18b3c8dd27320d35f402084c18222575e36d5a1b7beeb9c2b1fdc878f66bcd4757e70973212281d94be330bad505524543cf30ff46fb4f8b", new Guid("71b98be6-db33-4c32-ac00-ad6653b841d5") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "schema_dongle_simulator",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "Email", "Password", "UserIdentifier" },
                values: new object[] { new DateTime(2024, 8, 9, 3, 55, 7, 231, DateTimeKind.Utc).AddTicks(4192), "dgl_sim@email.com", "f07b238d966fc8be903e3c3a78b083a8e85554c4f9ee894889e0d6b336ce2834d214fdbacf745d10ff740c81e25124723c4764bf591704a4270461326f35b1d7", new Guid("25e0cbe2-b164-43ee-a27a-0fc79520f9ed") });
        }
    }
}
