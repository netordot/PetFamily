using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixforbreedfkcoloumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds");

            migrationBuilder.DropIndex(
                name: "ix_breeds_species_id",
                table: "breeds");

            migrationBuilder.DropColumn(
                name: "species_id1",
                table: "breeds");

            migrationBuilder.CreateIndex(
                name: "ix_breeds_species_id",
                table: "breeds",
                column: "species_id");

            migrationBuilder.AddForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds",
                column: "species_id",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds");

            migrationBuilder.DropIndex(
                name: "ix_breeds_species_id",
                table: "breeds");

            migrationBuilder.AddColumn<Guid>(
                name: "species_id1",
                table: "breeds",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_breeds_species_id",
                table: "breeds",
                column: "species_id1");

            migrationBuilder.AddForeignKey(
                name: "fk_breeds_species_species_id",
                table: "breeds",
                column: "species_id1",
                principalTable: "species",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
