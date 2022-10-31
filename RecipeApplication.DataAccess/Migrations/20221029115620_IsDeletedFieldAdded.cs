using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeApplication.DataAccess.Migrations
{
    public partial class IsDeletedFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Ingredients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Ingredients");
        }
    }
}
