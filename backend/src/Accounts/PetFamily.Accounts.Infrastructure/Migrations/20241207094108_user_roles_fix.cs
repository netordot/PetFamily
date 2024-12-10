using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Accounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class user_roles_fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                schema: "accounts",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "fk_asp_net_user_roles_asp_net_users_user_id",
                schema: "accounts",
                table: "AspNetUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_asp_net_user_roles",
                schema: "accounts",
                table: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "accounts",
                newName: "user_roles",
                newSchema: "accounts");

            migrationBuilder.RenameIndex(
                name: "ix_asp_net_user_roles_role_id",
                schema: "accounts",
                table: "user_roles",
                newName: "ix_user_roles_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_user_roles",
                schema: "accounts",
                table: "user_roles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_user_roles_asp_net_users_user_id",
                schema: "accounts",
                table: "user_roles",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_user_roles_roles_role_id",
                schema: "accounts",
                table: "user_roles",
                column: "role_id",
                principalSchema: "accounts",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_user_roles_asp_net_users_user_id",
                schema: "accounts",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "fk_user_roles_roles_role_id",
                schema: "accounts",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "pk_user_roles",
                schema: "accounts",
                table: "user_roles");

            migrationBuilder.RenameTable(
                name: "user_roles",
                schema: "accounts",
                newName: "AspNetUserRoles",
                newSchema: "accounts");

            migrationBuilder.RenameIndex(
                name: "ix_user_roles_role_id",
                schema: "accounts",
                table: "AspNetUserRoles",
                newName: "ix_asp_net_user_roles_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_asp_net_user_roles",
                schema: "accounts",
                table: "AspNetUserRoles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                schema: "accounts",
                table: "AspNetUserRoles",
                column: "role_id",
                principalSchema: "accounts",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_asp_net_user_roles_asp_net_users_user_id",
                schema: "accounts",
                table: "AspNetUserRoles",
                column: "user_id",
                principalSchema: "accounts",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
