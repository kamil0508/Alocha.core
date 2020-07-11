using Microsoft.EntityFrameworkCore.Migrations;

namespace Alocha.Domain.Migrations
{
    public partial class RemoveTableCustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sow_Customer_CustomerId",
                table: "Sow");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Sow_CustomerId",
                table: "Sow");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Sow");

            migrationBuilder.DropColumn(
                name: "Result",
                table: "Sow");

            migrationBuilder.AddColumn<bool>(
                name: "IsVaccinated",
                table: "Sow",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Sow",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sow_UserId",
                table: "Sow",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sow_AspNetUsers_UserId",
                table: "Sow",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sow_AspNetUsers_UserId",
                table: "Sow");

            migrationBuilder.DropIndex(
                name: "IX_Sow_UserId",
                table: "Sow");

            migrationBuilder.DropColumn(
                name: "IsVaccinated",
                table: "Sow");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sow");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Sow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                table: "Sow",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Regon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Customer_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sow_CustomerId",
                table: "Sow",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customer_UserId",
                table: "Customer",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Sow_Customer_CustomerId",
                table: "Sow",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
