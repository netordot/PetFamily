using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class volunteer_read_schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "volunteers");

            migrationBuilder.RenameTable(
                name: "volunteers",
                newName: "volunteers",
                newSchema: "volunteers");

            migrationBuilder.RenameTable(
                name: "pets",
                newName: "pets",
                newSchema: "volunteers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "volunteers",
                schema: "volunteers",
                newName: "volunteers");

            migrationBuilder.RenameTable(
                name: "pets",
                schema: "volunteers",
                newName: "pets");
        }
    }
}
