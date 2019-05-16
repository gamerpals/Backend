using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GamerPalsBackend.Migrations
{
    public partial class GlobalParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "GlobalParamsGlobalParametersID",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GlobalParameters",
                columns: table => new
                {
                    GlobalParametersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<bool>(nullable: true),
                    Country = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalParameters", x => x.GlobalParametersID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GlobalParamsGlobalParametersID",
                table: "Users",
                column: "GlobalParamsGlobalParametersID");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GlobalParameters_GlobalParamsGlobalParametersID",
                table: "Users",
                column: "GlobalParamsGlobalParametersID",
                principalTable: "GlobalParameters",
                principalColumn: "GlobalParametersID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_GlobalParameters_GlobalParamsGlobalParametersID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "GlobalParameters");

            migrationBuilder.DropIndex(
                name: "IX_Users_GlobalParamsGlobalParametersID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GlobalParamsGlobalParametersID",
                table: "Users");

            migrationBuilder.AddColumn<byte>(
                name: "Age",
                table: "Users",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Gender",
                table: "Users",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
