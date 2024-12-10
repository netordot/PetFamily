using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class names_write_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fisrt_name",
                schema: "accounts",
                table: "participant_accounts",
                newName: "first_name");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "accounts",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "accounts",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "middle_name",
                schema: "accounts",
                table: "users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "accounts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "accounts",
                table: "users");

            migrationBuilder.DropColumn(
                name: "middle_name",
                schema: "accounts",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "first_name",
                schema: "accounts",
                table: "participant_accounts",
                newName: "fisrt_name");
        }
    }
}
