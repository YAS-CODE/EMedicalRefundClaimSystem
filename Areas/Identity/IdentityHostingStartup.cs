using System;
using EMedicalClaimRefundSystem.Areas.Identity.Data;
using EMedicalClaimRefundSystem.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(EMedicalClaimRefundSystem.Areas.Identity.IdentityHostingStartup))]
namespace EMedicalClaimRefundSystem.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<EMedicalClaimRefundSystemContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("EMedicalClaimRefundSystemContextConnection")));

                /*services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<EMedicalClaimRefundSystemContext>();*/
            });

        }
    }
}