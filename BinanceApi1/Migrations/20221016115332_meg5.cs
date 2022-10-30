using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BinanceApi1.Migrations
{
    public partial class meg5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buyConditions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tradingPairId = table.Column<int>(nullable: false),
                    percentBalance = table.Column<float>(nullable: false),
                    maxInvestment = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buyConditions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_buyConditions_tradingPairs_tradingPairId",
                        column: x => x.tradingPairId,
                        principalTable: "tradingPairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_buyConditions_tradingPairId",
                table: "buyConditions",
                column: "tradingPairId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "buyConditions");
        }
    }
}
