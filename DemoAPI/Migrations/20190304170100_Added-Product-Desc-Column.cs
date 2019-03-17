using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoAPI.Migrations
{
    public partial class AddedProductDescColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "products",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "products");
        }
    }
}
