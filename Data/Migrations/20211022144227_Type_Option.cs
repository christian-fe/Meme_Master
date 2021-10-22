using Microsoft.EntityFrameworkCore.Migrations;

namespace Memes.Migrations
{
    public partial class Type_Option : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Photo",
                newName: "Option");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Option",
                table: "Photo",
                newName: "Type");
        }
    }
}
