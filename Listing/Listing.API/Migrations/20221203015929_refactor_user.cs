using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Listing.API.Migrations
{
    public partial class refactor_user : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Institution",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UrlImage",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Institution",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UrlImage",
                table: "Users");
        }
    }
}
