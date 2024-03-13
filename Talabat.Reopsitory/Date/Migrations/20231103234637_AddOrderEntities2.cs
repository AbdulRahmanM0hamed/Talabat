using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Reopsitory.Date.Migrations
{
    public partial class AddOrderEntities2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_deliveryMethods_orders_OrderId",
                table: "deliveryMethods");

            migrationBuilder.DropIndex(
                name: "IX_deliveryMethods_OrderId",
                table: "deliveryMethods");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "deliveryMethods");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "ordersItems",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryMethodId",
                table: "orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int?");

            migrationBuilder.CreateIndex(
                name: "IX_ordersItems_OrderId",
                table: "ordersItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ordersItems_orders_OrderId",
                table: "ordersItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordersItems_orders_OrderId",
                table: "ordersItems");

            migrationBuilder.DropIndex(
                name: "IX_ordersItems_OrderId",
                table: "ordersItems");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ordersItems");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryMethodId",
                table: "orders",
                type: "int?",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "deliveryMethods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_deliveryMethods_OrderId",
                table: "deliveryMethods",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_deliveryMethods_orders_OrderId",
                table: "deliveryMethods",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");
        }
    }
}
