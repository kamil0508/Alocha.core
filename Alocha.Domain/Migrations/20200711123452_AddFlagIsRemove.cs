using Microsoft.EntityFrameworkCore.Migrations;

namespace Alocha.Domain.Migrations
{
    public partial class AddFlagIsRemove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Sow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "SmallPig",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Sow");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "SmallPig");
        }
    }
}
