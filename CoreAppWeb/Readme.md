1. Tag Helper
- HTML Pre -Parsed Attribute system in ASP.NET Core
- they are used for Model binding from Server-to-Client for HTML elements
- e.g.	
	asp-action --> Used to generate Http Request for Action Method for MVC Controller
	asp-controller --> Http Request for MVC COntroller
	asp-for --> Model Binding of Primptive type property from Model class
	asp-items --> To Load IEnumerable on View for Rendering with Model Binding
	asp-route
	asp-route-id
2. Identity Scafolder
	- Microsoft.AspNetCore.Identity
		- IdentityUser
			- User Metadata
		- IdentityRole
			- Role Metadata
		- UserManager<IdentityUser>
			- Used to Manage Application Users
		- RoleManager<IdentityRole>
			- Used to Manage Application Roles
			- Define Users for Roles
		- SignInManager
			- Used to Manage Application Login Process using IdentityUser and IdentityRole 
	- Microsoft.AspNetCore.Identity.EntityFramework
		- IdentityDbContext
			- Genetrates ASP.NET Security Database Tables 
			- Provides Mechanism of Password Hash
			- Manages UsersInRole
			- Manage User/Role Claims
3. The 'PageModel' class is the Base class for WebForms in ASP.NET Core
	- This also provide the Model bindiong linke MVC to Views.
	- This also provide the Model bindiong linke MVC to Views.



	using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using RBS_AngularGuards.Models;
using RBS_AngularGuards.Repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace RBS_AngularGuards
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
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(Configuration
                    .GetConnectionString("AuthDbContextConnection"));
            });

            services.AddDbContext<AppNgDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("AppNgDbContextConnection"));
            });
            services.AddTransient<SecurityService>();

            //services.AddIdentityCore<IdentityUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<AuthDbContext>()
            //    .AddDefaultTokenProviders();

            services.AddIdentity<IdentityUser, IdentityRole>()
                    .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            //services.AddDefaultIdentity<IdentityUser>()
            //        .AddRoles<IdentityRole>()
            //        .AddEntityFrameworkStores<AuthDbContext>()
            //        .AddDefaultTokenProviders();


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
                // 1. Load the JST Secret Key to Verify and Validate Token
                // read key from appsettings.json
                var secretKey = Convert.FromBase64String(Configuration["JWTAppSettings:SecretKey"]);
                // 2. Defining the Mechanism for Validating Received Token from Client
                options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(secretKey)
               };
           });
            services.AddScoped<IRepository<Orders, int>, OrdersRepository>();
            

            services.AddMvc()
               .AddJsonOptions(options => options.SerializerSettings.ContractResolver
               = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IServiceProvider serv)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
            CreateAdministrator(serv).Wait();
            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }

        private async Task CreateAdministrator(IServiceProvider serviceProvider)
        {
            try
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

                IdentityResult result;
                // add a new Administrator role for the application
                var isRoleExist = await roleManager.RoleExistsAsync("Administrator");
                if (!isRoleExist)
                {
                    // create Administrator Role and add it in Database
                    result = await roleManager.CreateAsync(new IdentityRole("Administrator"));
                }

                // code to create a default user and add it to Administrator Role
                var user = await userManager.FindByEmailAsync("mahesh@myorg.com");
                if (user == null)
                {
                    var defaultUser = new IdentityUser() { UserName = "mahesh@myorg.com", Email = "mahesh@myorg.com" };
                    var regUser = await userManager.CreateAsync(defaultUser, "P@ssw0rd_");
                    await userManager.AddToRoleAsync(defaultUser, "Administrator");
                }
            }
            catch (Exception ex)
            {
                var str = ex.Message;
            }
           
        }
    }
}

