using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixedVOconfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "volunteer_requisites",
                table: "volunteers",
                newName: "requisites");

            migrationBuilder.RenameColumn(
                name: "pet_requisites",
                table: "pets",
                newName: "requisites");

            migrationBuilder.AddColumn<string>(
                name: "emails",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "name_last_name",
                table: "volunteers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_middle_name",
                table: "volunteers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name_name",
                table: "volunteers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emails",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "name_last_name",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "name_middle_name",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "name_name",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "volunteers",
                newName: "volunteer_requisites");

            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "pets",
                newName: "pet_requisites");
        }
    }
}
