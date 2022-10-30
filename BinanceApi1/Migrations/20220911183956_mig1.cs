using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BinanceApi1.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tradeOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    pair = table.Column<string>(nullable: true),
                    quantity = table.Column<decimal>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    tradeType = table.Column<int>(nullable: false),
                    BinanceOrderId = table.Column<int>(nullable: false),
                    ClientOrderId = table.Column<string>(nullable: true),
                    IsOpen = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tradeOrders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tradingPairs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tradingPairs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tradeOrders");

            migrationBuilder.DropTable(
                name: "tradingPairs");
        }
    }
}
