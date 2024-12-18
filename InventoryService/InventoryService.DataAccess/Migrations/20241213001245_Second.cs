using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InventoryAmount",
                table: "Inventories",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Inventories",
                newName: "InventoryAmount");
        }
    }
}
