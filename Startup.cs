using System;
using System.Threading.Tasks;
using EMedicalClaimRefundSystem.Areas.Identity.Data;
using EMedicalClaimRefundSystem.Models;
using EMedicalClaimRefundSystem.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace EMedicalClaimRefundSystem
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


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
            options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<EMedicalClaimRefundSystemContext>().AddDefaultTokenProviders();


            services.AddTransient<IEmailSender, EmailSender>(i =>
                new EmailSender(
                    Configuration["EmailSender:Host"],
                    Configuration.GetValue<int>("EmailSender:Port"),
                    Configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    Configuration["EmailSender:UserName"],
                    Configuration["EmailSender:Password"]
                )
            );
            //Password Strength Setting
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });

            //Setting the Account Login page
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Identity/Account/Login"; // If the LoginPath is not set here,
                                                      // ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Identity/Account/Logout"; // If the LogoutPath is not set here,
                                                        // ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // If the AccessDeniedPath is
                                                                    // not set here, ASP.NET Core 
                                                                    // will default to 
                                                                    // /Account/AccessDenied
                options.SlidingExpiration = true;
            });
            

            services.AddTransient<IApplicationHistoryRepo>(x => new ApplicationHistoryRepo(Configuration.GetConnectionString("EMedicalClaimRefundSystemContextConnection")));
            services.AddTransient<IERefundUserApplicationRepo>(x => new ERefundUserApplicationRepo(Configuration.GetConnectionString("EMedicalClaimRefundSystemContextConnection")));
            services.AddAntiforgery(x => x.HeaderName = "X-XSRF-TOKEN");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc();

            
            CreateUserRoles(services).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;
            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            //Adding User Role
            roleCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("User"));
            }
            //Adding Accountant Role
            roleCheck = await RoleManager.RoleExistsAsync("Accountant");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Accountant"));
            }
            //Adding Accountant Role
            roleCheck = await RoleManager.RoleExistsAsync("CRD");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("CRD"));
            }
            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            /*ApplicationUser user = await UserManager.FindByEmailAsync("itsyasir_1@hotmail.com");            
            await UserManager.AddToRoleAsync(user, "Admin");*/


            const string USERNAME = "system@admin.com";
            const string PASSWORD = "!@#Admin123";
            const string ROLENAME = "Admin";


            ApplicationUser user = await UserManager.FindByNameAsync(USERNAME);

            if (user == null)
            {
                var serviceUser = new ApplicationUser
                {
                    UserName = USERNAME,
                    Email = USERNAME,
                    Name = "Admin"                    
                };

                var createPowerUser = UserManager.CreateAsync(serviceUser, PASSWORD).Result;
                if (createPowerUser.Succeeded)
                {
                    var confirmationToken = UserManager.GenerateEmailConfirmationTokenAsync(serviceUser).Result;
                    var result = UserManager.ConfirmEmailAsync(serviceUser, confirmationToken).Result;
                    //here we tie the new user to the role
                    UserManager.AddToRoleAsync(serviceUser, ROLENAME).GetAwaiter().GetResult();
                }
            }
        }
    }
}
