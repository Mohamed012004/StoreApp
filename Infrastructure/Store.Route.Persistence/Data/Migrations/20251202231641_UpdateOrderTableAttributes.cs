using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Route.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderTableAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMethods_DeleveryMethodId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeleveryMethodId",
                table: "Orders",
                newName: "DeliveryMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeleveryMethodId",
                table: "Orders",
                newName: "IX_Orders_DeliveryMethodId");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders",
                column: "DeliveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryMethods_DeliveryMethodId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveryMethodId",
                table: "Orders",
                newName: "DeleveryMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryMethodId",
                table: "Orders",
                newName: "IX_Orders_DeleveryMethodId");

            migrationBuilder.AlterColumn<string>(
                name: "Quantity",
                table: "OrderItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryMethods_DeleveryMethodId",
                table: "Orders",
                column: "DeleveryMethodId",
                principalTable: "DeliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
