using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApi.Migrations
{
    /// <inheritdoc />
    public partial class trytouseCommunityEntityinPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "communityId",
                table: "Posts",
                newName: "Communityid");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_Communityid",
                table: "Posts",
                column: "Communityid");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Communities_Communityid",
                table: "Posts",
                column: "Communityid",
                principalTable: "Communities",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Communities_Communityid",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_Communityid",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Communityid",
                table: "Posts",
                newName: "communityId");
        }
    }
}
