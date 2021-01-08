using Microsoft.EntityFrameworkCore.Migrations;

namespace POSSystemWithInventory.Migrations
{
    public partial class stockTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProcutId",
                table: "StockDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcutId",
                table: "StockDetail",
                type: "int",
                nullable: true);
        }
    }
}
