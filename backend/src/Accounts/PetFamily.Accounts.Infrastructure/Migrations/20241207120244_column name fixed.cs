using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class columnnamefixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                schema: "accounts",
                table: "participant_accounts",
                newName: "fisrt_name");

            migrationBuilder.RenameColumn(
                name: "name",
                schema: "accounts",
                table: "admin_accounts",
                newName: "fisrt_name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "fisrt_name",
                schema: "accounts",
                table: "participant_accounts",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "fisrt_name",
                schema: "accounts",
                table: "admin_accounts",
                newName: "name");
        }
    }
}
