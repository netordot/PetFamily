using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class columnnamesfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds");

            migrationBuilder.RenameColumn(
                name: "name_name",
                table: "volunteers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "name_middle_name",
                table: "volunteers",
                newName: "middle_name");

            migrationBuilder.RenameColumn(
                name: "name_last_name",
                table: "volunteers",
                newName: "last_name");

            migrationBuilder.RenameColumn(
                name: "adress_street",
                table: "volunteers",
                newName: "street");

            migrationBuilder.RenameColumn(
                name: "adress_crops_number",
                table: "volunteers",
                newName: "crops_number");

            migrationBuilder.RenameColumn(
                name: "adress_city",
                table: "volunteers",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "adress_building_number",
                table: "volunteers",
                newName: "building_number");

            migrationBuilder.RenameColumn(
                name: "species_breed_species_id",
                table: "pets",
                newName: "SpeciesId");

            migrationBuilder.RenameColumn(
                name: "species_breed_breed_id",
                table: "pets",
                newName: "BreedId");

            migrationBuilder.RenameColumn(
                name: "adress_street",
                table: "pets",
                newName: "address_street");

            migrationBuilder.RenameColumn(
                name: "adress_crops_number",
                table: "pets",
                newName: "address_crops_number");

            migrationBuilder.RenameColumn(
                name: "adress_city",
                table: "pets",
                newName: "address_city");

            migrationBuilder.RenameColumn(
                name: "adress_building_number",
                table: "pets",
                newName: "address_building_number");

            migrationBuilder.AlterColumn<Guid>(
                name: "species_id",
                table: "breeds",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds",
                column: "species_id",
                principalTable: "species",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "volunteers",
                newName: "name_name");

            migrationBuilder.RenameColumn(
                name: "middle_name",
                table: "volunteers",
                newName: "name_middle_name");

            migrationBuilder.RenameColumn(
                name: "last_name",
                table: "volunteers",
                newName: "name_last_name");

            migrationBuilder.RenameColumn(
                name: "street",
                table: "volunteers",
                newName: "adress_street");

            migrationBuilder.RenameColumn(
                name: "crops_number",
                table: "volunteers",
                newName: "adress_crops_number");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "volunteers",
                newName: "adress_city");

            migrationBuilder.RenameColumn(
                name: "building_number",
                table: "volunteers",
                newName: "adress_building_number");

            migrationBuilder.RenameColumn(
                name: "SpeciesId",
                table: "pets",
                newName: "species_breed_species_id");

            migrationBuilder.RenameColumn(
                name: "BreedId",
                table: "pets",
                newName: "species_breed_breed_id");

            migrationBuilder.RenameColumn(
                name: "address_street",
                table: "pets",
                newName: "adress_street");

            migrationBuilder.RenameColumn(
                name: "address_crops_number",
                table: "pets",
                newName: "adress_crops_number");

            migrationBuilder.RenameColumn(
                name: "address_city",
                table: "pets",
                newName: "adress_city");

            migrationBuilder.RenameColumn(
                name: "address_building_number",
                table: "pets",
                newName: "adress_building_number");

            migrationBuilder.AlterColumn<Guid>(
                name: "species_id",
                table: "breeds",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds",
                column: "species_id",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
