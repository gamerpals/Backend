using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class mtonfix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Languages_Users_UserID",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_Languages_UserID",
                table: "Languages");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Languages");

            migrationBuilder.CreateTable(
                name: "UserLanguages",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false),
                    LanguageID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLanguages", x => new { x.LanguageID, x.UserID });
                    table.ForeignKey(
                        name: "FK_UserLanguages_Languages_LanguageID",
                        column: x => x.LanguageID,
                        principalTable: "Languages",
                        principalColumn: "LanguageID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLanguages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLanguages_UserID",
                table: "UserLanguages",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLanguages");

            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Languages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_UserID",
                table: "Languages",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Languages_Users_UserID",
                table: "Languages",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
