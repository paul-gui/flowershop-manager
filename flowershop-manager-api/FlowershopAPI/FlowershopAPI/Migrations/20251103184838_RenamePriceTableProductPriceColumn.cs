using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlowerShopAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenamePriceTableProductPriceColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductPrice",
                table: "Prices",
                newName: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Prices",
                newName: "ProductPrice");
        }
    }
}
