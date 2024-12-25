using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.VolunteerRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class experiencefieldadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "experience",
                schema: "volunteer_requests",
                table: "volunteer_requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "experience",
                schema: "volunteer_requests",
                table: "volunteer_requests");
        }
    }
}
