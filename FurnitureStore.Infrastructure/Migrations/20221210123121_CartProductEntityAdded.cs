using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Infrastructure.Migrations
{
    public partial class CartProductEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "money", precision: 18, scale: 2, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99858a34-d71e-40c5-b550-7f78f07d5a48",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "651a4da2-ab6f-4f8d-955b-fd899b892675", "AQAAAAEAACcQAAAAEE6EOm3imkLKFGH9W0Kd9aEfEIrA6c7/1dxxh+Jouz4cyY9Hd5AJgen0JhUS4rrgIw==", "5851196d-412d-473f-b5e3-7cca12c8accf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e8bb923e-291c-467c-993f-1126f5fc1502", "AQAAAAEAACcQAAAAEP9WXio0IWzp0otWq68ZO4VR0tfLr+o7e0lEv7cEMR1m81+1P4rzPjzUEgSSSi2ErQ==", "3a58aec6-ac6c-4e33-8d9b-4f0c813aae84" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BuyerId",
                table: "Products",
                column: "BuyerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99858a34-d71e-40c5-b550-7f78f07d5a48",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b9c2e9b-5455-4e58-9ab9-f0aacfed0ba6", "AQAAAAEAACcQAAAAEL4bzYD2RgRqJM1eJYmqc/+qir32PQRlb5yPGhCDxTU2ABKFdxY9nbNQjlvIfjyY2A==", "c691975e-bb85-461e-b280-82728a4a950c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b22c1eeb-19e2-4ec5-b7fd-d6d935c0c54e", "AQAAAAEAACcQAAAAED2Xlwa0oAhtadC3WmRNjBpMud5CCpTtCwDdlAvjwsWF65Yc8C50FNOK8BUWkTR1ow==", "5f4b26ad-3882-4725-84ee-971a2a6c7bc7" });
        }
    }
}
