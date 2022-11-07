using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApplication.DataAccess.Migrations
{
    public partial class RecipeCreatedByFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Recipes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Recipes");
        }
    }
}
