using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class LoginAdaptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "GoogleID",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GoogleID",
                table: "Users",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
