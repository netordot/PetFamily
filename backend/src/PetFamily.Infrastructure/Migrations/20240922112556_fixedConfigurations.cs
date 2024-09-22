using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedConfigurations : Migration
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
                newName: "volunteer_requisites");

            migrationBuilder.RenameColumn(
                name: "Requisites",
                table: "pets",
                newName: "pet_requisites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "pet_photos",
                newName: "PetPhotos");

            migrationBuilder.RenameColumn(
                name: "volunteer_requisites",
                table: "volunteers",
                newName: "Requisites");

            migrationBuilder.RenameColumn(
                name: "pet_requisites",
                table: "pets",
                newName: "Requisites");
        }
    }
}
