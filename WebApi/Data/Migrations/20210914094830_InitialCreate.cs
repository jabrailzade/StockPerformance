using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SymbolAggregates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    SymbolName = table.Column<string>(type: "TEXT", nullable: true),
                    ClosePrice = table.Column<float>(type: "REAL", nullable: false),
                    HighestPrice = table.Column<float>(type: "REAL", nullable: false),
                    LowestPrice = table.Column<float>(type: "REAL", nullable: false),
                    NumberOfTransactions = table.Column<long>(type: "INTEGER", nullable: false),
                    OpenPrice = table.Column<float>(type: "REAL", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SymbolAggregates", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SymbolAggregates");
        }
    }
}
