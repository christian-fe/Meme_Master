using Microsoft.EntityFrameworkCore.Migrations;

namespace Memes.Migrations
{
    public partial class Category_category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Photo",
                newName: "category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "category",
                table: "Photo",
                newName: "Category");
        }
    }
}
