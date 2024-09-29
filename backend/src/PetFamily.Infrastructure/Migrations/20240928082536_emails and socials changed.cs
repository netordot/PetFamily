using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class emailsandsocialschanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "number_number",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_number_number",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "number_number",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "phone_number_number",
                table: "pets");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "pets",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
