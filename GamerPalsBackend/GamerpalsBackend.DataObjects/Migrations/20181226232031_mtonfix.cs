using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class mtonfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ActiveSearches_ActiveSearchID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ActiveSearchID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ActiveSearchID",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ActiveSearchUsers",
                columns: table => new
                {
                    ActiveSearchID = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSearchUsers", x => new { x.ActiveSearchID, x.UserID });
                    table.ForeignKey(
                        name: "FK_ActiveSearchUsers_ActiveSearches_ActiveSearchID",
                        column: x => x.ActiveSearchID,
                        principalTable: "ActiveSearches",
                        principalColumn: "ActiveSearchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveSearchUsers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSearchUsers_UserID",
                table: "ActiveSearchUsers",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveSearchUsers");

            migrationBuilder.AddColumn<int>(
                name: "ActiveSearchID",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActiveSearchID",
                table: "Users",
                column: "ActiveSearchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ActiveSearches_ActiveSearchID",
                table: "Users",
                column: "ActiveSearchID",
                principalTable: "ActiveSearches",
                principalColumn: "ActiveSearchID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
