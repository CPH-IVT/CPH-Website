using CPH.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPH.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Counties> Counties { get; set; }
        public DbSet<DatapointColumns> DatapointColumns { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<RegionCounties> RegionCounties { get; set; }
        public DbSet<SavedChartCounties> SavedChartCounties { get; set; }
        public DbSet<SavedChartDatapoints> SavedChartDatapoints { get; set; }
        public DbSet<SavedChartRegions> SavedChartRegions { get; set; }
        public DbSet<SavedCharts> SavedCharts { get; set; }
        public DbSet<SavedChartYear> SavedChartYear { get; set; }
        public DbSet<States> States { get; set; }

    }
}
