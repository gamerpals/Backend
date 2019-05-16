using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SearchTypes",
                columns: table => new
                {
                    SearchTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SearchTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchTypes", x => x.SearchTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActiveSearchID = table.Column<int>(type: "int", nullable: true),
                    LeaderUserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupID);
                });

            migrationBuilder.CreateTable(
                name: "SearchSettings",
                columns: table => new
                {
                    SearchSettingsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActiveSearchID = table.Column<int>(type: "int", nullable: true),
                    ParameterID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchSettings", x => x.SearchSettingsID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActiveSearchID = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<byte>(type: "tinyint", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacebookID = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<byte>(type: "tinyint", nullable: false),
                    GoogleID = table.Column<int>(type: "int", nullable: false),
                    Karma = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentSearch = table.Column<int>(type: "int", nullable: false),
                    GameName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlayersOnline = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameID);
                    table.ForeignKey(
                        name: "FK_Games_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LangLong = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangShort = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageID);
                    table.ForeignKey(
                        name: "FK_Languages_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    ParameterID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GameID = table.Column<int>(type: "int", nullable: true),
                    ParameterName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParameterValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.ParameterID);
                    table.ForeignKey(
                        name: "FK_Parameters_Games_GameID",
                        column: x => x.GameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Servers",
                columns: table => new
                {
                    GameServerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ServerGameGameID = table.Column<int>(type: "int", nullable: true),
                    ServerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.GameServerID);
                    table.ForeignKey(
                        name: "FK_Servers_Games_ServerGameGameID",
                        column: x => x.ServerGameGameID,
                        principalTable: "Games",
                        principalColumn: "GameID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActiveSearches",
                columns: table => new
                {
                    ActiveSearchID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    MaxPlayers = table.Column<int>(type: "int", nullable: false),
                    SearchTypeID = table.Column<int>(type: "int", nullable: true),
                    ServerGameServerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveSearches", x => x.ActiveSearchID);
                    table.ForeignKey(
                        name: "FK_ActiveSearches_SearchTypes_SearchTypeID",
                        column: x => x.SearchTypeID,
                        principalTable: "SearchTypes",
                        principalColumn: "SearchTypeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActiveSearches_Servers_ServerGameServerID",
                        column: x => x.ServerGameServerID,
                        principalTable: "Servers",
                        principalColumn: "GameServerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSearches_SearchTypeID",
                table: "ActiveSearches",
                column: "SearchTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveSearches_ServerGameServerID",
                table: "ActiveSearches",
                column: "ServerGameServerID");

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserID",
                table: "Games",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ActiveSearchID",
                table: "Groups",
                column: "ActiveSearchID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_LeaderUserID",
                table: "Groups",
                column: "LeaderUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_UserID",
                table: "Languages",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Parameters_GameID",
                table: "Parameters",
                column: "GameID");

            migrationBuilder.CreateIndex(
                name: "IX_SearchSettings_ActiveSearchID",
                table: "SearchSettings",
                column: "ActiveSearchID");

            migrationBuilder.CreateIndex(
                name: "IX_SearchSettings_ParameterID",
                table: "SearchSettings",
                column: "ParameterID");

            migrationBuilder.CreateIndex(
                name: "IX_Servers_ServerGameGameID",
                table: "Servers",
                column: "ServerGameGameID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ActiveSearchID",
                table: "Users",
                column: "ActiveSearchID");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Users_LeaderUserID",
                table: "Groups",
                column: "LeaderUserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_ActiveSearches_ActiveSearchID",
                table: "Groups",
                column: "ActiveSearchID",
                principalTable: "ActiveSearches",
                principalColumn: "ActiveSearchID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchSettings_ActiveSearches_ActiveSearchID",
                table: "SearchSettings",
                column: "ActiveSearchID",
                principalTable: "ActiveSearches",
                principalColumn: "ActiveSearchID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchSettings_Parameters_ParameterID",
                table: "SearchSettings",
                column: "ParameterID",
                principalTable: "Parameters",
                principalColumn: "ParameterID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ActiveSearches_ActiveSearchID",
                table: "Users",
                column: "ActiveSearchID",
                principalTable: "ActiveSearches",
                principalColumn: "ActiveSearchID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSearches_SearchTypes_SearchTypeID",
                table: "ActiveSearches");

            migrationBuilder.DropForeignKey(
                name: "FK_ActiveSearches_Servers_ServerGameServerID",
                table: "ActiveSearches");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "SearchSettings");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropTable(
                name: "SearchTypes");

            migrationBuilder.DropTable(
                name: "Servers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ActiveSearches");
        }
    }
}
