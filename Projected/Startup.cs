using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Projected.Data.LDAP;
using Projected.Data.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projected
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
            services.AddDbContext<ProjectProfileContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("ProfileConnection"))
            );

            //Cookie Authentication
            string loginPath = "/Account/SignIn";
            string accessDeniedPath = "/Account/Unauthorized";
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = loginPath;
                options.AccessDeniedPath = accessDeniedPath;
            });

            var envrionment = Configuration["Environment"];
            if (!string.Equals(envrionment, "LOCALHOST", StringComparison.InvariantCultureIgnoreCase))
            {
                services.AddDataProtection()
                    .PersistKeysToFileSystem(new DirectoryInfo("ProjectedKeyStore"))
                    .SetApplicationName("Projected");
            }

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddControllersWithViews();

            services.AddTransient<FncConnectToGroupLdapBehavior>();
            services.AddTransient(x => new List<IConnectToGroupLdapBehavior>()
            {
                x.GetService<FncConnectToGroupLdapBehavior>()
            });
            services.AddTransient<IConnectToAuthLdapBehavior, CoreLogicConnectToAuthLdapBehavior>();
            services.AddTransient<IGetLdapGroupsBehavior, GetLdapGroupsBehavior>();
            services.AddScoped<IProjectProfileRepo, ProjectProfileRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
