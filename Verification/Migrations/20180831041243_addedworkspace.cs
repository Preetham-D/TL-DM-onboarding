using Microsoft.EntityFrameworkCore.Migrations;

namespace Verification.Migrations
{
    public partial class addedworkspace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Workspace",
                table: "User",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Workspace",
                table: "User");
        }
    }
}
