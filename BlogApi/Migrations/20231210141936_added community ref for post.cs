using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class addedcommunityrefforpost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Posts_communityId",
                table: "Posts",
                column: "communityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Communities_communityId",
                table: "Posts",
                column: "communityId",
                principalTable: "Communities",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Communities_communityId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_communityId",
                table: "Posts");
        }
    }
}
