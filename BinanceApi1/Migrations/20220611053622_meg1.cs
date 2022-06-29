using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BinanceApi1.Migrations
{
    public partial class meg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "marketPrints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    max = table.Column<decimal>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    min = table.Column<decimal>(nullable: false),
                    time = table.Column<DateTime>(nullable: false),
                    pair = table.Column<string>(nullable: true),
                    pricePercentPosition = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marketPrints", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "marketPrints");
        }
    }
}
