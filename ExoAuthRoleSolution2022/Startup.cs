using ExoAuthEtRoleSolution.Authorization;
using ExoAuthRoleSolution2022.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExoAuthRoleSolution2022
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

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options =>
                options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddScoped<IAuthorizationHandler, VetementProprietaireAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, VetementAdminAuthorizationHandler>();
            services.AddControllersWithViews();
            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            // Authentification via Microsoft Azure https://portal.azure.com
            // Documentation : https://docs.microsoft.com/fr-ca/aspnet/core/security/authentication/social/microsoft-logins?view=aspnetcore-3.1
            /*services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                // Si les cl?s sont dans usersecrets.json
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];

                // Si les cl?s sont dans appsettings.json
                //microsoftOptions.ClientId = Configuration.GetSection("AuthenticationMicrosoft:ClientId").Value;
                //microsoftOptions.ClientSecret = Configuration.GetSection("AuthenticationMicrosoft:ClientSecret").Value;
            });*/
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Vetements}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
