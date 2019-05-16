using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class RolesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserOptions_UserOptionsId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserOptionsId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserOptionsId",
                table: "Roles");

            migrationBuilder.RenameColumn(
                name: "UserOptionsId",
                table: "UserOptions",
                newName: "UserOptionsID");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "RoleID");

            migrationBuilder.CreateTable(
                name: "UserOptionRoles",
                columns: table => new
                {
                    UserOptionId = table.Column<int>(nullable: false),
                    UserOptionsID = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOptionRoles", x => new { x.UserOptionId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserOptionRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOptionRoles_UserOptions_UserOptionsID",
                        column: x => x.UserOptionsID,
                        principalTable: "UserOptions",
                        principalColumn: "UserOptionsID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserOptionRoles_RoleId",
                table: "UserOptionRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOptionRoles_UserOptionsID",
                table: "UserOptionRoles",
                column: "UserOptionsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserOptionRoles");

            migrationBuilder.RenameColumn(
                name: "UserOptionsID",
                table: "UserOptions",
                newName: "UserOptionsId");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.AddColumn<int>(
                name: "UserOptionsId",
                table: "Roles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserOptionsId",
                table: "Roles",
                column: "UserOptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserOptions_UserOptionsId",
                table: "Roles",
                column: "UserOptionsId",
                principalTable: "UserOptions",
                principalColumn: "UserOptionsId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
