﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Species.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class species_schema_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "species");

            migrationBuilder.RenameTable(
                name: "species",
                newName: "species",
                newSchema: "species");

            migrationBuilder.RenameTable(
                name: "breeds",
                newName: "breeds",
                newSchema: "species");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "species",
                schema: "species",
                newName: "species");

            migrationBuilder.RenameTable(
                name: "breeds",
                schema: "species",
                newName: "breeds");
        }
    }
}
