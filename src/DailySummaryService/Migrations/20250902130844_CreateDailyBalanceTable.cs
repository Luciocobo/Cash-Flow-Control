using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailySummaryService.Migrations
{
    /// <inheritdoc />
    public partial class CreateDailyBalanceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyBalances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalCredits = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalDebits = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyBalances", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DailyBalances_Date",
                table: "DailyBalances",
                column: "Date",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyBalances");
        }
    }
}
