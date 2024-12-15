using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Discussion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class discussion_write_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "users_admin_id",
                schema: "discussions",
                table: "discussions",
                newName: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "user_id",
                schema: "discussions",
                table: "discussions",
                newName: "users_admin_id");
        }
    }
}
