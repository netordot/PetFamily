﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Discussion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class discussion_read_initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "discussions");

            migrationBuilder.CreateTable(
                name: "discussions",
                schema: "discussions",
                columns: table => new
                {
                    id1 = table.Column<Guid>(type: "uuid", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    admin_id = table.Column<Guid>(type: "uuid", nullable: false),
                    relation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discussions", x => x.id1);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                schema: "discussions",
                columns: table => new
                {
                    id1 = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_edited = table.Column<bool>(type: "boolean", nullable: false),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    discussion_id = table.Column<Guid>(type: "uuid", nullable: false),
                    discussion_dto_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id1);
                    table.ForeignKey(
                        name: "fk_messages_discussions_discussion_dto_id",
                        column: x => x.discussion_dto_id,
                        principalSchema: "discussions",
                        principalTable: "discussions",
                        principalColumn: "id1");
                });

            migrationBuilder.CreateIndex(
                name: "ix_messages_discussion_dto_id",
                schema: "discussions",
                table: "messages",
                column: "discussion_dto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages",
                schema: "discussions");

            migrationBuilder.DropTable(
                name: "discussions",
                schema: "discussions");
        }
    }
}