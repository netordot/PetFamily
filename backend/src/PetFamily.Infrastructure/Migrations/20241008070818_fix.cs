using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address_crops_number",
                table: "pets");

            migrationBuilder.AddColumn<int>(
                name: "experience",
                table: "volunteers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "pets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_of_birth",
                table: "pets",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "health_condition",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "height",
                table: "pets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "is_castrated",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_vaccinated",
                table: "pets",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "pets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "weight",
                table: "pets",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "experience",
                table: "volunteers");

            migrationBuilder.DropColumn(
                name: "color",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "date_of_birth",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "health_condition",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "height",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_castrated",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "is_vaccinated",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "status",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "weight",
                table: "pets");

            migrationBuilder.AddColumn<int>(
                name: "address_crops_number",
                table: "pets",
                type: "integer",
                nullable: true);
        }
    }
}
