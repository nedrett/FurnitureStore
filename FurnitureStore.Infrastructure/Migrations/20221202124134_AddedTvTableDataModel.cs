using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Infrastructure.Migrations
{
    public partial class AddedTvTableDataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TvTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Width = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Length = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "money", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BuyerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TvTables_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TvTables_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_TvTables_BuyerId",
                table: "TvTables",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TvTables_CreatorId",
                table: "TvTables",
                column: "CreatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TvTables");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99858a34-d71e-40c5-b550-7f78f07d5a48",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fd58026-f5a5-49f2-aa28-dcf19d49aa99", "AQAAAAEAACcQAAAAECoqrXPeMo1Bs9LmRqr9pOMPb4HVQjkPIIsJh3y0jOns7HN7Ewn337qjY2QbVOlfag==", "7d13091c-d60d-4325-8070-701e3f66424e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e194159-d3d1-49d6-a6d5-ba46f2bff8eb", "AQAAAAEAACcQAAAAEADQjxUQOFFTNG6a3Akw+VbZNlwNx1jS5j2M7NhsAxe0jh3vkH7WPVulTb93rS3+GA==", "0c4eee0f-f0cc-4f58-b714-4fdea04c1e87" });
        }
    }
}
