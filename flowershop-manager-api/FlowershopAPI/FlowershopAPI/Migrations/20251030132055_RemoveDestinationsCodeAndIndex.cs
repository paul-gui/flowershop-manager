using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowershopAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDestinationsCodeAndIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Destinations_Code",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Destinations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Destinations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Code",
                value: "SHOP");

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Code",
                value: "ENGROS");

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_Code",
                table: "Destinations",
                column: "Code",
                unique: true);
        }
    }
}
