using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class ParameterFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchSettings_ActiveSearches_ActiveSearchID",
                table: "SearchSettings");

            migrationBuilder.DropIndex(
                name: "IX_SearchSettings_ActiveSearchID",
                table: "SearchSettings");

            migrationBuilder.DropColumn(
                name: "ActiveSearchID",
                table: "SearchSettings");

            migrationBuilder.RenameColumn(
                name: "ParameterValue",
                table: "Parameters",
                newName: "ParameterType");

            migrationBuilder.CreateTable(
                name: "SearchParameters",
                columns: table => new
                {
                    ParameterID = table.Column<int>(nullable: false),
                    ActiveSearchID = table.Column<int>(nullable: false),
                    ParameterValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchParameters", x => new { x.ActiveSearchID, x.ParameterID });
                    table.ForeignKey(
                        name: "FK_SearchParameters_ActiveSearches_ActiveSearchID",
                        column: x => x.ActiveSearchID,
                        principalTable: "ActiveSearches",
                        principalColumn: "ActiveSearchID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SearchParameters_Parameters_ParameterID",
                        column: x => x.ParameterID,
                        principalTable: "Parameters",
                        principalColumn: "ParameterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchParameters_ParameterID",
                table: "SearchParameters",
                column: "ParameterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchParameters");

            migrationBuilder.RenameColumn(
                name: "ParameterType",
                table: "Parameters",
                newName: "ParameterValue");

            migrationBuilder.AddColumn<int>(
                name: "ActiveSearchID",
                table: "SearchSettings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SearchSettings_ActiveSearchID",
                table: "SearchSettings",
                column: "ActiveSearchID");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchSettings_ActiveSearches_ActiveSearchID",
                table: "SearchSettings",
                column: "ActiveSearchID",
                principalTable: "ActiveSearches",
                principalColumn: "ActiveSearchID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
