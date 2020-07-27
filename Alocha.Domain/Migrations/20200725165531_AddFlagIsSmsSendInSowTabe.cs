using Microsoft.EntityFrameworkCore.Migrations;

namespace Alocha.Domain.Migrations
{
    public partial class AddFlagIsSmsSendInSowTabe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSmsSend",
                table: "Sow",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSmsSend",
                table: "Sow");
        }
    }
}
