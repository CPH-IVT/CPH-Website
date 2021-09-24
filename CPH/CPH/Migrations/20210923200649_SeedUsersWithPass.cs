using Microsoft.EntityFrameworkCore.Migrations;

namespace CPH.Migrations
{
    public partial class SeedUsersWithPass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "98d58fae-c1be-4da1-a05f-a1df67d0fa69", "AQAAAAEAACcQAAAAEAgIQJJ075zSCI64ukEkvtspFPVvC7DW3v28PJJQ7qHs2D6xo1U5EVQ7x+XfGMRJ5g==", "551f96d6-1c1f-44bb-84ac-5f4f0718a0e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af8ccf87-2ab5-4be3-9bf5-c422ee785e82",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6e9ee479-4960-40f0-8a70-b7cb9469e276", "AQAAAAEAACcQAAAAELaSJd/MpwWbmi3oQ4t4l21ETallN1VkeLCB0BXbYkAGsTpt+qzwwh9jrIuP0Gyaag==", "66459aab-e7bf-44f4-89c1-6b88a15da628" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a6b4ba4f-1dd1-4ab0-9d03-16979d777ca9", null, "bc98a7be-a715-44e2-b5c5-211ceca60afb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af8ccf87-2ab5-4be3-9bf5-c422ee785e82",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "769ebcde-8cd3-4063-9065-da7213f40dfc", null, "d2bfd401-dbbe-4188-8ea6-772cd54c950b" });
        }
    }
}
