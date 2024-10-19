using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class onemorepetfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date_of_birth",
                table: "pets",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "pets",
                newName: "created_date");

            migrationBuilder.RenameColumn(
                name: "SpeciesId",
                table: "pets",
                newName: "species_id");

            migrationBuilder.RenameColumn(
                name: "BreedId",
                table: "pets",
                newName: "breed_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "species_id",
                table: "pets",
                newName: "SpeciesId");

            migrationBuilder.RenameColumn(
                name: "created_date",
                table: "pets",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "breed_id",
                table: "pets",
                newName: "BreedId");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "pets",
                newName: "date_of_birth");
        }
    }
}
