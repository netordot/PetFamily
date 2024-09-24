using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Configfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PetPhotos",
                newName: "pet_photos");

            migrationBuilder.RenameColumn(
                name: "Requisites",
                table: "volunteers",
                newName: "requisites");

            migrationBuilder.RenameColumn(
                name: "Requisites",
                table: "pets",
                newName: "requisites");

            migrationBuilder.AddColumn<int>(
                name: "adress_building_number",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "adress_city",
                table: "volunteers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "adress_crops_number",
                table: "volunteers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adress_street",
                table: "volunteers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.AddColumn<int>(
                name: "adress_building_number",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "adress_city",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "adress_crops_number",
                table: "pets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "adress_street",
                table: "pets",
                type: "character varying(100)",
                maxLength: 100,
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
                name: "adress_building_number",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "adress_city",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "adress_crops_number",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "adress_street",
                table: "volunteers");

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
                name: "adress_building_number",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "adress_city",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "adress_crops_number",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "adress_street",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "pets");

            migrationBuilder.RenameTable(
                name: "pet_photos",
                newName: "PetPhotos");

            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "volunteers",
                newName: "Requisites");

            migrationBuilder.RenameColumn(
                name: "requisites",
                table: "pets",
                newName: "Requisites");
        }
    }
}
