using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Infrastructure.Migrations
{
    public partial class TvTableDataModelSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "TvTables",
                columns: new[] { "Id", "BuyerId", "CreatorId", "Description", "Height", "ImageUrl", "IsActive", "Length", "Name", "Price", "Quantity", "Width" },
                values: new object[] { 1, null, "99858a34-d71e-40c5-b550-7f78f07d5a48", "Floor Type Tv Table", 0.74m, "https://www.ikea.com/nl/en/images/products/besta-tv-bench-with-doors-and-drawers-black-brown-lappviken-stubbarp-black-brown__0719166_pe731898_s5.jpg?f=s", true, 0.42m, "Tv Bench", 175m, 1, 2.4m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TvTables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99858a34-d71e-40c5-b550-7f78f07d5a48",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "75fce322-5aef-4d58-bb18-cdb67259c8e1", "AQAAAAEAACcQAAAAEPr6l+gPf42oWNRv5kViM560aTOKgn7Qn4qv7Soc6W7ZiXFVwtaXK3oI32Yl8nR6kA==", "e34f2caa-0b1e-418c-bf44-0d2107ae0fbc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "266a1680-23b3-4bba-bc7c-b42c440c07a5", "AQAAAAEAACcQAAAAEOTi+0CeP68wqUbEbgC33dS71sQCLwXlQ2wdv+T4QWpEf4ylFzxrIbjfRFlGkXtYpQ==", "1d1efbd4-7336-42fd-b0c6-d8ee52c612fa" });
        }
    }
}
