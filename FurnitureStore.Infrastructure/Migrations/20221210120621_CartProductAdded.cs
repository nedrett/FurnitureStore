using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Infrastructure.Migrations
{
    public partial class CartProductAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99858a34-d71e-40c5-b550-7f78f07d5a48",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "45dfd754-9931-4857-b34d-d6b559dff80f", "AQAAAAEAACcQAAAAEGGBorPoPEQptBfNLrTPN44DBYwQLUzFDeg05KAB2cZJ5GwmACAANSMVVlKNgsKfWg==", "1ec25cb9-2081-426c-8186-d2fbdf737d12" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d28bb660-71af-4edd-aae3-6b06d6a09140", "AQAAAAEAACcQAAAAEAhpgII6BDlcey14Q5EHG7GJwXWAGfEqKZ4V82OrD/xYHhrg4HWW8niBgu14C83e6Q==", "a735e0d2-32ac-45f8-80e9-897976016dc3" });
        }
    }
}
