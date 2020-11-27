using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ecommerceAspCore.Models;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ecommerceAspCore
{
    public class Startup
    {
        /* ---- New Changes ---- */
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            /* ---- New Changes ---- */
            // for authentication cookieAdmin
            services.AddAuthentication("cookieAdmin")
                .AddCookie("cookieAdmin", config => 
                {
                    config.LoginPath  = "/admin/login/index";
                    config.LogoutPath = "/admin/login/signout";
                    config.AccessDeniedPath = "/admin/account/accessdenied";
                });
            services.AddSession();
            // for Add Model View Controller
            services.AddMvc();
            // for database mysql
            services.AddDbContext<DatabaseContext>(options => 
            {
                options.UseMySql(configuration.GetConnectionString("MySql"),ServerVersion.AutoDetect(configuration.GetConnectionString("MySql")));
                options.UseLazyLoadingProxies();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            /**
             * ========= new Changes ========
             * app.UseSession()
             * app.UseAuthentication() to authenticate
             * app.UseAuthorization() to authorization
            */
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
