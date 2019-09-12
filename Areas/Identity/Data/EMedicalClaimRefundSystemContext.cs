using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EMedicalClaimRefundSystem.Areas.Identity.Data;

namespace EMedicalClaimRefundSystem.Models
{
    public class EMedicalClaimRefundSystemContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ERefundUserApplication> ERefundUserApplication { get; set; }
        public DbSet<ApplicationHistory> ApplicationHistory { get; set; }
        public EMedicalClaimRefundSystemContext(DbContextOptions<EMedicalClaimRefundSystemContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
