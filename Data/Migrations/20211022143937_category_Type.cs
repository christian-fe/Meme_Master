using Microsoft.EntityFrameworkCore.Migrations;

namespace Memes.Migrations
{
    public partial class category_Type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemeName",
                table: "Photo",
                newName: "PhotoName");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Photo",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "MemeId",
                table: "Photo",
                newName: "PhotoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Photo",
                newName: "Category");

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
