using Microsoft.EntityFrameworkCore.Migrations;

namespace Alocha.Domain.Migrations
{
    public partial class AddIsEditColumnInSowTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEdit",
                table: "Sow",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdit",
                table: "Sow");
        }
    }
}
