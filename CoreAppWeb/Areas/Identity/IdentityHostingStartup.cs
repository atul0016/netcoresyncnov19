using System;
using CoreAppWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CoreAppWeb.Areas.Identity.IdentityHostingStartup))]
namespace CoreAppWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //    services.AddDbContext<SecurityContext>(options =>
            //        options.UseSqlServer(
            //            context.Configuration.GetConnectionString("SecurityContextConnection")));

            //    services.AddDefaultIdentity<IdentityUser>()
            //        .AddEntityFrameworkStores<SecurityContext>();
            //});
        }
    }
}