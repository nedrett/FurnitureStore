using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurnitureStore.Infrastructure.Migrations
{
    public partial class DataModelsSeedingAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "99858a34-d71e-40c5-b550-7f78f07d5a48", 0, "9fd58026-f5a5-49f2-aa28-dcf19d49aa99", "user@mail.com", false, false, null, "USER@MAIL.COM", "USER@MAIL.COM", "AQAAAAEAACcQAAAAECoqrXPeMo1Bs9LmRqr9pOMPb4HVQjkPIIsJh3y0jOns7HN7Ewn337qjY2QbVOlfag==", null, false, "7d13091c-d60d-4325-8070-701e3f66424e", false, "user@mail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "dea12856-c198-4129-b3f3-b893d8395082", 0, "9e194159-d3d1-49d6-a6d5-ba46f2bff8eb", "admin@mail.com", false, false, null, "ADMIN@MAIL.COM", "ADMIN@MAIL.COM", "AQAAAAEAACcQAAAAEADQjxUQOFFTNG6a3Akw+VbZNlwNx1jS5j2M7NhsAxe0jh3vkH7WPVulTb93rS3+GA==", null, false, "0c4eee0f-f0cc-4f58-b714-4fdea04c1e87", false, "admin@mail.com" });

            migrationBuilder.InsertData(
                table: "ArmChairs",
                columns: new[] { "Id", "BuyerId", "CreatorId", "Description", "Height", "ImageUrl", "IsActive", "Length", "Name", "Price", "Quantity", "UpholsteryType", "Width" },
                values: new object[] { 1, null, "99858a34-d71e-40c5-b550-7f78f07d5a48", "Best Armchair", 1.3m, "https://assets.pbimgs.com/pbimgs/rk/images/dp/wcm/202229/0183/burton-upholstered-armchair-navy-c.jpg", true, 1m, "Roll arm Armchair", 120m, 1, "Fiber", 1m });

            migrationBuilder.InsertData(
                table: "Chairs",
                columns: new[] { "Id", "BuyerId", "CreatorId", "Description", "ImageUrl", "IsActive", "Name", "Price", "Quantity" },
                values: new object[] { 1, null, "99858a34-d71e-40c5-b550-7f78f07d5a48", "Best dining chair", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJbFFCH0CQvVrrVcbKLuQToSX9XW1exfJ3ne1EjgMXyIOvXgCU4XqA4F7BLzAV8RnF1mw&usqp=CAU", true, "Dining Chair", 50m, 4 });

            migrationBuilder.InsertData(
                table: "Sofas",
                columns: new[] { "Id", "BuyerId", "CreatorId", "Description", "Height", "ImageUrl", "IsActive", "Length", "Name", "Price", "Quantity", "UpholsteryType", "Width" },
                values: new object[] { 1, null, "99858a34-d71e-40c5-b550-7f78f07d5a48", "Leather 3 Seater Sofa", 0.85m, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcShd6D0eit4uZ_i9wgxK1bSQEm-Aqijsi0Tsr-JwxoDfzAJn2F1cgnY6BP7DyTPOdE9g_o&usqp=CAU", true, 1.2m, "Classic Sofa", 350m, 1, "Leather", 3m });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "BuyerId", "CreatorId", "Description", "ImageUrl", "IsActive", "Length", "Material", "Price", "Quantity", "Type", "Width" },
                values: new object[] { 1, null, "99858a34-d71e-40c5-b550-7f78f07d5a48", "Best Dining Table", "https://c.media.kavehome.com/images/Products/CC0006M40_1V01.jpg?tx=w_900,c_fill,ar_0.8,q_auto", true, 0.75m, "Wood", 800m, 1, "Dining Table", 2m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ArmChairs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");

            migrationBuilder.DeleteData(
                table: "Chairs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sofas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tables",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "99858a34-d71e-40c5-b550-7f78f07d5a48");
        }
    }
}
