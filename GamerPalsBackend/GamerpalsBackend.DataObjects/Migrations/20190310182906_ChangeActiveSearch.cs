using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class ChangeActiveSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ActiveSearches",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserID",
                table: "ActiveSearches",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSearches_OwnerUserID",
                table: "ActiveSearches",
                column: "OwnerUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_ActiveSearches_Users_OwnerUserID",
                table: "ActiveSearches",
                column: "OwnerUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSearches_Users_OwnerUserID",
                table: "ActiveSearches");

            migrationBuilder.DropIndex(
                name: "IX_ActiveSearches_OwnerUserID",
                table: "ActiveSearches");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ActiveSearches");

            migrationBuilder.DropColumn(
                name: "OwnerUserID",
                table: "ActiveSearches");
        }
    }
}
