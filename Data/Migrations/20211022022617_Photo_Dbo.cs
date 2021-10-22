using Microsoft.EntityFrameworkCore.Migrations;

namespace Memes.Migrations
{
    public partial class Photo_Dbo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemeName",
                table: "Photo",
                newName: "PhotoName");

            migrationBuilder.RenameColumn(
                name: "MemeId",
                table: "Photo",
                newName: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhotoName",
                table: "Photo",
                newName: "MemeName");

            migrationBuilder.RenameColumn(
                name: "PhotoId",
                table: "Photo",
                newName: "MemeId");
        }
    }
}
