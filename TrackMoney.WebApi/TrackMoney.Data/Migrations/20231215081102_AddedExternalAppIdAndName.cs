using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrackMoney.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedExternalAppIdAndName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExternalAppId",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalAppType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalAppId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ExternalAppType",
                table: "Users");
        }
    }
}
