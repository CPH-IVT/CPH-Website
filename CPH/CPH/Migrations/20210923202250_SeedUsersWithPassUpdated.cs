///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         20210923202250_SeedUsersWithPassUpdated.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            CSCI 2210 - Data Structures	
//	Author:           DESKTOP-FOTV38D\Joshua, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         DESKTOP-FOTV38D\Joshua, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    /// <summary>
    /// Defines the <see cref="SeedUsersWithPassUpdated" />.
    /// </summary>
    public partial class SeedUsersWithPassUpdated : Migration
    {
        /// <summary>
        /// The Up.
        /// </summary>
        /// <param name="migrationBuilder">The migrationBuilder<see cref="MigrationBuilder"/>.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23323371-d725-483b-b38a-65b0b451ad2b", "AQAAAAEAACcQAAAAEAOWHbURECwG1oniTXRJRRlnctMnu2oM2j7Mx9om9wXwqMXQhQLaGJR6BXSyqRjGkw==", "2533b70b-7c8f-469b-8935-7a448b88c360" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af8ccf87-2ab5-4be3-9bf5-c422ee785e82",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b5e62fd5-6d94-4f2b-95da-24e2384b90ba", "AQAAAAEAACcQAAAAEOg42KSjPeRhqHcr7ws/pDfboXBgcSEXNiDfwsFU00b52jL+0RUDacKJ+/sRrOHxKQ==", "f32c362f-7926-4fe2-9a73-432d09ea664b" });
        }

        /// <summary>
        /// The Down.
        /// </summary>
        /// <param name="migrationBuilder">The migrationBuilder<see cref="MigrationBuilder"/>.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
