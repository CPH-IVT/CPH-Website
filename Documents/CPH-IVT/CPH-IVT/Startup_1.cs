using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using CPH_IVT.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using CPH_IVT.Services.MongoDB;
using CPH_IVT.Services.MongoDB.Repository;

namespace CPH_IVT
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            // MongoDB stuff
            //
            // requires using Microsoft.Extensions.Options
            //services.Configure<HealthDatabaseSettings>(
            //    Configuration.GetSection(nameof(HealthDatabaseSettings)));

            //services.AddSingleton<IHealthDatabaseSettings>(sp =>
            //    sp.GetRequiredService<IOptions<HealthDatabaseSettings>>().Value);

            //services.AddSingleton<HealthIndicatorContext>();

            services.AddMvc();

            services.Configure<HealthDatabaseSettings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("HealthDatabaseSettings:ConnectionString").Value;
                    options.DatabaseName = Configuration.GetSection("HealthDatabaseSettings:DatabaseName").Value;
                });

            services.AddTransient<IHealthIndicatorContext, HealthIndicatorContext>();
            services.AddTransient<IHealthIndicatorRepository, HealthIndicatorRepository>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

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
