using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Infrastructure.Migrations
{
    public partial class TableDbModelColumnRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Tables",
                newName: "Name");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tables",
                newName: "Type");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99858a34-d71e-40c5-b550-7f78f07d5a48",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8a3bf63c-7c48-41fa-b6e8-d9e16b8b88c5", "AQAAAAEAACcQAAAAEGeU7fBe4BHKcdEYd6hy4XUWdY6n961wvdkTib94ix4tkSVKawQcaHLsiazVQLfm7A==", "fb497c44-9871-4b1e-9388-e1940de6991c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf6927c6-3f5b-4049-843d-9a4634201fe1", "AQAAAAEAACcQAAAAED7J2WZTCUyVL2r0+SNF/ihLSxrrHxosXNDEnhkiuESVY2seYYzn+ayFnBJyqdV+Sw==", "7f6ab648-5eef-4bf7-9c53-c2134081c933" });
        }
    }
}
