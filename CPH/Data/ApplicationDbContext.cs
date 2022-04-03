///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ApplicationDbContext.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Data
{
    using CPH.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Defines the <see cref="ApplicationDbContext" />.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{ApplicationDbContext}"/>.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The OnModelCreating.
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed db with admin user and cartographer
            AdminAndCartographerSeed(builder);

            // Seed db with Roles
            RolesSeed(builder);

            // Seed db with User Roles
            UserRolesSeeding(builder);

            // Seed db with state information
            SeedDbWithStateInfo(builder);
        }

        /// <summary>
        /// The AdminAndCartographerSeed.
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/>.</param>
        private void AdminAndCartographerSeed(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8",
                UserName = "trimmj@etsu.edu",
                Email = "trimmj@etsu.edu",
                FirstName = "Joshua",
                LastName = "Trimm",
                PhoneNumber = "4233415125",
                NormalizedEmail = "TRIMMJ@ETSU.EDU",
                NormalizedUserName = "TRIMMJ@ETSU.EDU",
                EmailConfirmed = true

            };

            var cartUser = new ApplicationUser
            {
                Id = "af8ccf87-2ab5-4be3-9bf5-c422ee785e82",
                UserName = "jbthype@gmail.com",
                Email = "jbthype@gmail.com",
                FirstName = "Mariam",
                LastName = "Trimm",
                PhoneNumber = "4233415125",
                NormalizedEmail = "JBTHYPE@GMAIL.COM",
                NormalizedUserName = "JBTHYPE@GMAIL.COM",
                EmailConfirmed = true

            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Password1!");
            cartUser.PasswordHash = hasher.HashPassword(cartUser, "Password1!");

            builder.Entity<ApplicationUser>().HasData(adminUser, cartUser);
        }

        /// <summary>
        /// The RolesSeed.
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/>.</param>
        private void RolesSeed(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "0e9a83ad-8f2f-4ab9-947b-6d2c5d130d56",
                    Name = "Admin",
                    NormalizedName = "Admin",
                    ConcurrencyStamp = "1"
                },
                new IdentityRole
                {
                    Id = "0d8f650d-6c6c-4ace-b5b0-0af276006ea3",
                    Name = "Cartographer",
                    NormalizedName = "Cartographer",
                    ConcurrencyStamp = "2"
                }
                );
        }

        /// <summary>
        /// The UserRolesSeeding.
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/>.</param>
        private void UserRolesSeeding(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasData(

                new IdentityUserRole<string> // Adding admin role to admin account
                {
                    RoleId = "0e9a83ad-8f2f-4ab9-947b-6d2c5d130d56",
                    UserId = "9ba79920-3bac-4420-ba4d-4b0c9ddf6ef8"
                },
                new IdentityUserRole<string> // Adding Joshua Trimm account to Homeowner role
                {
                    RoleId = "0d8f650d-6c6c-4ace-b5b0-0af276006ea3",
                    UserId = "af8ccf87-2ab5-4be3-9bf5-c422ee785e82"
                }
                );
        }

        /// <summary>
        /// The SeedDbWithStateInfo.
        /// </summary>
        /// <param name="builder">The builder<see cref="ModelBuilder"/>.</param>
        private void SeedDbWithStateInfo(ModelBuilder builder)
        {
            builder.Entity<States>().HasData(
new States
{
    Id = 1,
    Abbreviation = "AL",
    Name = "ALABAMA"
},
new States
{
    Id = 2,
    Abbreviation = "AK",
    Name = "ALASKA"
},
new States
{
    Id = 4,
    Abbreviation = "AZ",
    Name = "ARIZONA"
},
new States
{
    Id = 5,
    Abbreviation = "AR",
    Name = "ARKANSAS"
},
new States
{
    Id = 6,
    Abbreviation = "CA",
    Name = "CALIFORNIA"
},
new States
{
    Id = 8,
    Abbreviation = "CO",
    Name = "COLORADO"
},
new States
{
    Id = 9,
    Abbreviation = "CT",
    Name = "CONNECTICUT"
},
new States
{
    Id = 10,
    Abbreviation = "DE",
    Name = "DELAWARE"
},
new States
{
    Id = 12,
    Abbreviation = "FL",
    Name = "FLORIDA"
},
new States
{
    Id = 13,
    Abbreviation = "GA",
    Name = "GEORGIA"
},
new States
{
    Id = 15,
    Abbreviation = "HI",
    Name = "HAWAII"
},
new States
{
    Id = 16,
    Abbreviation = "ID",
    Name = "IDAHO"
},
new States
{
    Id = 17,
    Abbreviation = "IL",
    Name = "ILLINOIS"
},
new States
{
    Id = 18,
    Abbreviation = "IN",
    Name = "INDIANA"
},
new States
{
    Id = 19,
    Abbreviation = "IA",
    Name = "IOWA"
},
new States
{
    Id = 20,
    Abbreviation = "KS",
    Name = "KANSAS"
},
new States
{
    Id = 21,
    Abbreviation = "KY",
    Name = "KENTUCKY"
},
new States
{
    Id = 22,
    Abbreviation = "LA",
    Name = "LOUISIANA"
},
new States
{
    Id = 23,
    Abbreviation = "ME",
    Name = "MAINE"
},
new States
{
    Id = 24,
    Abbreviation = "MD",
    Name = "MARYLAND"
},
new States
{
    Id = 25,
    Abbreviation = "MA",
    Name = "MASSACHUSETTS"
},
new States
{
    Id = 26,
    Abbreviation = "MI",
    Name = "MICHIGAN"
},
new States
{
    Id = 27,
    Abbreviation = "MN",
    Name = "MINNESOTA"
},
new States
{
    Id = 28,
    Abbreviation = "MS",
    Name = "MISSISSIPPI"
},
new States
{
    Id = 29,
    Abbreviation = "MO",
    Name = "MISSOURI"
},
new States
{
    Id = 30,
    Abbreviation = "MT",
    Name = "MONTANA"
},
new States
{
    Id = 31,
    Abbreviation = "NE",
    Name = "NEBRASKA"
},
new States
{
    Id = 32,
    Abbreviation = "NV",
    Name = "NEVADA"
},
new States
{
    Id = 33,
    Abbreviation = "NH",
    Name = "NEW HAMPSHIRE"
},
new States
{
    Id = 34,
    Abbreviation = "NJ",
    Name = "NEW JERSEY"
},
new States
{
    Id = 35,
    Abbreviation = "NM",
    Name = "NEW MEXICO"
},
new States
{
    Id = 36,
    Abbreviation = "NY",
    Name = "NEW YORK"
},
new States
{
    Id = 37,
    Abbreviation = "NC",
    Name = "NORTH CAROLINA"
},
new States
{
    Id = 38,
    Abbreviation = "ND",
    Name = "NORTH DAKOTA"
},
new States
{
    Id = 39,
    Abbreviation = "OH",
    Name = "OHIO"
},
new States
{
    Id = 40,
    Abbreviation = "OK",
    Name = "OKLAHOMA"
},
new States
{
    Id = 41,
    Abbreviation = "OR",
    Name = "OREGON"
},
new States
{
    Id = 42,
    Abbreviation = "PA",
    Name = "PENNSYLVANIA"
},
new States
{
    Id = 44,
    Abbreviation = "RI",
    Name = "RHODE ISLAND"
},
new States
{
    Id = 45,
    Abbreviation = "SC",
    Name = "SOUTH CAROLINA"
},
new States
{
    Id = 46,
    Abbreviation = "SD",
    Name = "SOUTH DAKOTA"
},
new States
{
    Id = 47,
    Abbreviation = "TN",
    Name = "TENNESSEE"
},
new States
{
    Id = 48,
    Abbreviation = "TX",
    Name = "TEXAS"
},
new States
{
    Id = 49,
    Abbreviation = "UT",
    Name = "UTAH"
},
new States
{
    Id = 50,
    Abbreviation = "VT",
    Name = "VERMONT"
},
new States
{
    Id = 51,
    Abbreviation = "VA",
    Name = "VIRGINIA"
},
new States
{
    Id = 53,
    Abbreviation = "WA",
    Name = "WASHINGTON"
},
new States
{
    Id = 54,
    Abbreviation = "WV",
    Name = "WEST VIRGINIA"
},
new States
{
    Id = 55,
    Abbreviation = "WI",
    Name = "WISCONSIN"
},
new States
{
    Id = 56,
    Abbreviation = "WY",
    Name = "WYOMING"
},
new States
{
    Id = 60,
    Abbreviation = "AS",
    Name = "AMERICAN SAMOA"
},
new States
{
    Id = 66,
    Abbreviation = "GU",
    Name = "GUAM"
},
new States
{
    Id = 69,
    Abbreviation = "MP",
    Name = "NORTHERN MARIANA ISLANDS"
},
new States
{
    Id = 72,
    Abbreviation = "PR",
    Name = "PUERTO RICO"
},
new States
{
    Id = 78,
    Abbreviation = "VI",
    Name = "VIRGIN ISLANDS"
}
);
        }

        public DbSet<Chart> Chart { get; set; }

        /// <summary>
        /// Gets or sets the States.
        /// </summary>
        public DbSet<States> States { get; set; }
    }
}
