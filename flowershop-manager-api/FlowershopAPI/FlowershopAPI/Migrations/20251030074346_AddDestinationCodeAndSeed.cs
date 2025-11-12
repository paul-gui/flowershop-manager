using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FlowershopAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddDestinationCodeAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prices_ProductId",
                table: "Prices");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Destinations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Destinations",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "SHOP", "Florărie" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "ENGROS", "En Gros" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductId_DestinationId",
                table: "Prices",
                columns: new[] { "ProductId", "DestinationId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_Code",
                table: "Destinations",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Prices_ProductId_DestinationId",
                table: "Prices");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_Code",
                table: "Destinations");

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DropColumn(
                name: "Code",
                table: "Destinations");

            migrationBuilder.CreateIndex(
                name: "IX_Prices_ProductId",
                table: "Prices",
                column: "ProductId");
        }
    }
}
