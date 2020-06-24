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
using FoodREV.DataAccess;
using FoodREV.DataAccess.Data.Repository.IRepository;
using FoodREV.DataAccess.Data.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using FoodREV.Utility;
using Microsoft.Extensions.Options;
using Stripe;
using Microsoft.AspNetCore.Authentication.Facebook;

namespace FoodREV
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
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            services.AddIdentity<IdentityUser,IdentityRole>()
                .AddDefaultTokenProviders()
                //.AddDefaultUI(UIFramewor.bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddMvc(options => options.EnableEndpointRouting = false)
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            //services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = "1198004347209891";
                facebookOptions.AppSecret = "424ce1560906402e87c4b94c4b5dc6bf";
            });
            services.AddAuthentication().AddMicrosoftAccount(options =>
            {
                options.ClientId = "ec98b8fc-94f1-48e6-83c2-1363832ca84a";
                options.ClientSecret = "~E-ti3Z535cAG7_evU2g5g~hWDff9-7K0K";
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMvc();
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
        }
    }
}
