///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         20210923195558_SeedUsers.cs
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
    /// Defines the <see cref="SeedUsers" />.
    /// </summary>
    public partial class SeedUsers : Migration
    {
        /// <summary>
        /// The Up.
        /// </summary>
        /// <param name="migrationBuilder">The migrationBuilder<see cref="MigrationBuilder"/>.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e9a83ad-8f2f-4ab9-947b-6d2c5d130d56", "1", "Admin", "Admin" },
                    { "0d8f650d-6c6c-4ace-b5b0-0af276006ea3", "2", "Cartographer", "Cartographer" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "FirstName", "LastName" },
                values: new object[,]
                {
                    { "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8", 0, "a6b4ba4f-1dd1-4ab0-9d03-16979d777ca9", "ApplicationUser", "trimmj@etsu.edu", false, false, null, "TRIMMJ@ETSU.EDU", "TRIMMJ@ETSU.EDU", null, "4233415125", false, "bc98a7be-a715-44e2-b5c5-211ceca60afb", false, "trimmj@etsu.edu", "Joshua", "Trimm" },
                    { "af8ccf87-2ab5-4be3-9bf5-c422ee785e82", 0, "769ebcde-8cd3-4063-9065-da7213f40dfc", "ApplicationUser", "jbthype@gmail.com", false, false, null, "JBTHYPE@GMAIL.COM", "JBTHYPE@GMAIL.COM", null, "4233415125", false, "d2bfd401-dbbe-4188-8ea6-772cd54c950b", false, "jbthype@gmail.com", "Mariam", "Trimm" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8", "0e9a83ad-8f2f-4ab9-947b-6d2c5d130d56" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "af8ccf87-2ab5-4be3-9bf5-c422ee785e82", "0d8f650d-6c6c-4ace-b5b0-0af276006ea3" });
        }

        /// <summary>
        /// The Down.
        /// </summary>
        /// <param name="migrationBuilder">The migrationBuilder<see cref="MigrationBuilder"/>.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8", "0e9a83ad-8f2f-4ab9-947b-6d2c5d130d56" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "af8ccf87-2ab5-4be3-9bf5-c422ee785e82", "0d8f650d-6c6c-4ace-b5b0-0af276006ea3" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d8f650d-6c6c-4ace-b5b0-0af276006ea3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e9a83ad-8f2f-4ab9-947b-6d2c5d130d56");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "af8ccf87-2ab5-4be3-9bf5-c422ee785e82");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
