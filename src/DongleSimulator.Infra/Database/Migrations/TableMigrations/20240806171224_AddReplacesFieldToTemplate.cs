using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DongleSimulator.Infra.TableMigrations
{
    /// <inheritdoc />
    public partial class AddReplacesFieldToTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Replaces",
                schema: "schema_dongle_simulator",
                table: "Templates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Replaces",
                schema: "schema_dongle_simulator",
                table: "Templates");
        }
    }
}
