using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDestinationIdOnSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DestinationId",
                table: "Sales",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Sales_DestinationId",
                table: "Sales",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Destinations_DestinationId",
                table: "Sales",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Destinations_DestinationId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_DestinationId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Sales");
        }
    }
}
