using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.VolunteerRequest.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class secondnameremoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "second_name",
                schema: "volunteer_requests",
                table: "volunteer_requests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "second_name",
                schema: "volunteer_requests",
                table: "volunteer_requests",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
