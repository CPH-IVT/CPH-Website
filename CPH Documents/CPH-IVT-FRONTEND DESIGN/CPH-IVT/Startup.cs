using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using CPH_IVT.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Repository;
using CPH_IVT.Services.MongoDB.Init;
using CPH_IVT.Services.MongoDB.Context;

namespace CPH_IVT
{
    /// <summary>
    /// Provides configuration for CPH-IVT project.
    /// Boilerplate provided by ASP.NET Core.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="configuration"><see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// <see cref="IConfiguration"/>
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adds services to the DI and/or IOC containers at runtime.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SQLConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc();

            services.Configure<HealthDatabaseSettings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("HealthDatabaseSettings:ConnectionString").Value;
                    options.DatabaseName = Configuration.GetSection("HealthDatabaseSettings:DatabaseName").Value;
                });

            // Transients for all contexts
            //
            services.AddTransient<ICensusDivisionContext, CensusDivisionContext>();
            services.AddTransient<ICensusRegionContext, CensusRegionContext>();
            services.AddTransient<ICountyContext, CountyContext>();
            services.AddTransient<ICustomRegionContext, CustomRegionContext>();
            services.AddTransient<IHealthIndicatorContext, HealthIndicatorContext>();
            services.AddTransient<IStateContext, StateContext>();

            // Transients for all repositories
            //
            services.AddTransient<ICensusDivisionRepository, CensusDivisionRepository>();
            services.AddTransient<ICensusRegionRepository, CensusRegionRepository>();
            services.AddTransient<ICountyRepository, CountyRepository>();
            services.AddTransient<ICustomRegionRepository, CustomRegionRepository>();
            services.AddTransient<IHealthIndicatorRepository, HealthIndicatorRepository>();
            services.AddTransient<IStateRepository, StateRepository>();

            // Scoped for database initialization
            services.AddScoped<InitializerAsync>();
            

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        /// <summary>
        /// Configures the HTTP request pipeline at runtime.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        /// <param name="env"><see cref="IWebHostBuilder"/></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // TODO: Remove the above comment before deployment.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
