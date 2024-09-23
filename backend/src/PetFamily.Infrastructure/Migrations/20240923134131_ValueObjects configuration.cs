using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ValueObjectsconfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "volunteer_emails",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "volunteer_name",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "volunteer_phone_number",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "pet_phone_number",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "volunteer_emails",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "volunteer_name",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "volunteer_phone_number",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "pet_phone_number",
                table: "pets");
        }
    }
}
