using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class emailfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emails",
                table: "volunteers");

            migrationBuilder.AddColumn<string>(
                name: "email_mail",
                table: "volunteers",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_mail",
                table: "volunteers");

            migrationBuilder.AddColumn<string>(
                name: "emails",
                table: "volunteers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
