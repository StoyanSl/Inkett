﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Services;
using Inkett.Infrastructure.Data;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Infrastructure.Services;
using Inkett.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
                
            });
            services.AddDbContext<AppIdentityDbContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDbContext<InkettContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("InkettConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITattooService, TattooService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IImageUploader, ImageUploader>();
            services.AddScoped<IProfileViewModelService, ProfileViewModelService>();
            services.AddScoped<IAlbumViewModelService, AlbumViewModelService>();
            services.AddScoped<ITattooViewModelService, TattooViewModelService>();
            services.AddMvc(options=> { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        
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
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
