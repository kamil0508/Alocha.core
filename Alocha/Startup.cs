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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Alocha.Domain;
using Alocha.WebUi.Extensions;
using AutoMapper;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Alocha.WebUi.Helpers;
using Alocha.Domain.DbInitializer;

namespace Alocha
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
            // Add controllers and views handler
            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();


            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });

            // Add IdentityUser and IdentityRole
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddErrorDescriber<OverrideIdentityResultErrors>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
                
            services.AddAutoMapper(typeof(Startup));

            // Identity settings
            services.AddIdentitySettings();

            // Add Repository via Dependency Injection extension container
            services.RepositoryInjector();

            // Add Services via Dependency Injection extension container
            services.ServiceInjector();

            // Add Cookie authentication
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            //Access Denied path
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/LogIn";
            });

            //Add DBInitializer
            services.AddScoped<IDBInitializer, DbInitializer>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDBInitializer dbInitialize)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            CultureInfo newCulture = new CultureInfo("pl-PL");
            newCulture.NumberFormat.NumberDecimalSeparator = ".";
            newCulture.NumberFormat.NumberGroupSeparator = " ";
            newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            newCulture.DateTimeFormat.ShortTimePattern = "HH:mm:ss";
            newCulture.DateTimeFormat.FullDateTimePattern = "dd-MM-yyyy HH:mm:ss";
            newCulture.DateTimeFormat.DateSeparator = "-";
            newCulture.DateTimeFormat.TimeSeparator = ":";

            CultureInfo.DefaultThreadCurrentCulture = newCulture;
            CultureInfo.DefaultThreadCurrentUICulture = newCulture;
            CultureInfo.CurrentCulture = newCulture;
            CultureInfo.CurrentUICulture = newCulture;

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(newCulture),
                SupportedCultures = new List<CultureInfo>
                {
                    newCulture,
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    newCulture,
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LogIn}/{id?}");
                endpoints.MapRazorPages();
            });

            dbInitialize.Initializer();
        }
    }
}
