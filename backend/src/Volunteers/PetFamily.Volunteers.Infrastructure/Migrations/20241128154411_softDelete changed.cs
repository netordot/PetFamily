using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Volunteers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class softDeletechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_date",
                schema: "volunteers",
                table: "volunteers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "volunteers",
                table: "volunteers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "deletion_date",
                schema: "volunteers",
                table: "pets",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "volunteers",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "deletion_date",
                schema: "volunteers",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "volunteers",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "deletion_date",
                schema: "volunteers",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "volunteers",
                table: "pets");
        }
    }
}
