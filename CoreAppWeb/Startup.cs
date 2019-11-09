using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreAppWeb.Models;
using Microsoft.EntityFrameworkCore;
using CoreAppWeb.Services;
using CoreAppWeb.CusrtomFilters;
using Microsoft.AspNetCore.Identity;

namespace CoreAppWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. 
        //Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // register the SecurityContext class in DI Container
            services.AddDbContext<SecurityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SecurityConnection"));
            });
            // ends here

            // add the idenity service for creating and managing users for the application
            // this will provide instances for
            // 1. SignManager<IdentityUser>
            // 2.  UserManager<IdentityUser>
            //services.AddDefaultIdentity<IdentityUser>()
            //      .AddEntityFrameworkStores<SecurityContext>();

           
            // the user and role manager injectors
            services.AddIdentity<IdentityUser,IdentityRole>()
                .AddDefaultUI() // method for redirect url for security
                 .AddEntityFrameworkStores<SecurityContext>();
            // ends here

            // define access policy for role
            services.AddAuthorization(options => {
                options.AddPolicy("ReadPolicy", policy =>
                {
                    policy.RequireRole("Manager", "Clerk", "Operator");
                });
                options.AddPolicy("WritePolicy", policy =>
                {
                    policy.RequireRole("Manager");
                });
            });
            // ends users

            // register DbContext as service in DI Container
            // use the Code-First Migration to generate Database
            services.AddDbContext<SyncDbContext>(options=> {
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationConnection"));
            });
            // ends here

            // register the Service Classes aka Repository Classes
            services.AddScoped<IService<Category, int>, CategoryService>();
            services.AddScoped<IService<Product, int>, ProductService>();
            // ends here

            // register action filters
            services.AddMvc(options=> {
              //  options.Filters.Add(typeof(AppExceptionFilter));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
            app.UseAuthentication(); // the middleware for authentication

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
