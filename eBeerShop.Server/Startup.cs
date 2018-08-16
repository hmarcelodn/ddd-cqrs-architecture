using eBeerShop.Persistance.Facade;
using eBeerShop.Persistance.Identity;
using eBeerShop.Persistance.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace eBeerShop.Server
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Adding DomainModelFacade to Postgres SQL
            services.AddDbContext<DomainModelFacade>(options => {
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Adding ASPNet Core Identity
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                    .AddEntityFrameworkStores<DomainModelFacade>()
                    .AddDefaultTokenProviders();

            // Adding Seed (TBD: Move all to extension methods)
            services.AddScoped<SeedData, SeedData>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            // Adding ASPNet Core Identity Middleware
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Seed Identity Data
            SeedData.Run(app.ApplicationServices).Wait();
        }
    }
}
