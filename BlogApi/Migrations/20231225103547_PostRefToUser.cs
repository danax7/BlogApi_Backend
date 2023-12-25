using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class PostRefToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Authors_AuthorEntityId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_AuthorEntityId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorEntityId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_authorId",
                table: "Posts",
                column: "authorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Authors_authorId",
                table: "Posts",
                column: "authorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Authors_authorId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_authorId",
                table: "Posts");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorEntityId",
                table: "Posts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AuthorEntityId",
                table: "Posts",
                column: "AuthorEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Authors_AuthorEntityId",
                table: "Posts",
                column: "AuthorEntityId",
                principalTable: "Authors",
                principalColumn: "Id");
        }
    }
}
