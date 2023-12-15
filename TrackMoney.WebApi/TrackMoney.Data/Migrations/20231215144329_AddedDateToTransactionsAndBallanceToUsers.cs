using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackMoney.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateToTransactionsAndBallanceToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceInUAH",
                table: "Currencies",
                newName: "ValueInUAH");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "ValueInUAH",
                table: "Currencies",
                newName: "PriceInUAH");
        }
    }
}
