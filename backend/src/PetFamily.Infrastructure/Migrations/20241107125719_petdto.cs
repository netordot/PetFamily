using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class petdto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "pets",
                newName: "address_street");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "pets",
                newName: "phone_number_number");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "pets",
                newName: "address_city");

            migrationBuilder.AlterColumn<int>(
                name: "crops_number",
                table: "volunteers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "building_number",
                table: "volunteers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone_number_number",
                table: "pets",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "address_street",
                table: "pets",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "address_city",
                table: "pets",
                newName: "city");

            migrationBuilder.AlterColumn<string>(
                name: "crops_number",
                table: "volunteers",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "building_number",
                table: "volunteers",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
