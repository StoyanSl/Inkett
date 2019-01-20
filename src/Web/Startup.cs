using Inkett.ApplicationCore.Interfaces.Repositories;
using Inkett.ApplicationCore.Interfaces.Services;
using Inkett.ApplicationCore.Services;
using Inkett.Infrastructure.Data;
using Inkett.Infrastructure.Identity;
using Inkett.Web.Interfaces.Services;
using Inkett.Infrastructure.Services;
using Inkett.Web.Services;
using Inkett.Web.Handlers;
using Inkett.ApplicationCore.Services.Options;
using Inkett.Web.Common.Mapping;
using Inkett.ApplicationCore.Entitites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using System;
using Inkett.Web.Models.ViewModels.Profiles;
using AutoMapper;

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
            services.Configure<CloudinaryApiDetails>(Configuration.GetSection("CloudinaryApiDetails"));

           AutoMapperConfig.RegisterMappings(
               typeof(Tattoo).Assembly,
               typeof(ProfileViewModel).Assembly
           );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddUserManager<InkettUserManager>()
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

            services.AddAuthorization(options =>
            {
                options.AddPolicy("EditPolicy", policy =>
                    policy.Requirements.Add(new SameProfileRequirement()));
            });

            services.AddScoped<IAuthorizationHandler, ResourceAuthorizationHandler>();

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITattooService, TattooService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IImageUploader, ImageUploader>();
            services.AddScoped<IHomeViewModelService, HomeViewModelService>();
            services.AddScoped<IProfileViewModelService, ProfileViewModelService>();
            services.AddScoped<IAlbumViewModelService, AlbumViewModelService>();
            services.AddScoped<ITattooViewModelService, TattooViewModelService>();
            services.AddScoped<INotificationViewModelService, NotificationViewModelService>();
            services.AddScoped<IStyleService, StyleService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IStyleViewModelService, StyleViewModelService>();
            services.AddMemoryCache();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(30);
                options.Cookie.HttpOnly = true;
            });

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
            app.UseSession();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Manage}/{action=Index}/{id?}"
                );
            });
        }
    }
}
