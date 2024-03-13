using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Reopsitory.Date.Migrations
{
    public partial class UpdateDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_deliveryMethods_DeliveryMethodId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ordersItems_orders_OrderId",
                table: "ordersItems");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_deliveryMethods_DeliveryMethodId",
                table: "orders",
                column: "DeliveryMethodId",
                principalTable: "deliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ordersItems_orders_OrderId",
                table: "ordersItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orders_deliveryMethods_DeliveryMethodId",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ordersItems_orders_OrderId",
                table: "ordersItems");

            migrationBuilder.AddForeignKey(
                name: "FK_orders_deliveryMethods_DeliveryMethodId",
                table: "orders",
                column: "DeliveryMethodId",
                principalTable: "deliveryMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ordersItems_orders_OrderId",
                table: "ordersItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id");
        }
    }
}
