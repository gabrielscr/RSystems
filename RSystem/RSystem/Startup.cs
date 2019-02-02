using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RSystem.Properties;
using MediatR;
using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using RSystem.Infrastructure;

namespace RSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DataSettings = new DataSettings();
            Configuration.GetSection("ConnectionStrings").Bind(DataSettings);
        }

        private DataSettings DataSettings { get; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services
                .AddMvc()
                .AddFeatureFolders()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<AdminContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("RSystemConnection")));


            services.AddMediatR(typeof(Startup));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            var supportedCultures = new[]
            {
                new CultureInfo("pt-BR")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void RegisterValidators(IServiceCollection services)
        {
            var assembly = typeof(Startup).GetTypeInfo().Assembly;
            var validatorType = typeof(IValidator);

            var validators = assembly
                .GetExportedTypes()
                .Where(t => validatorType.IsAssignableFrom(t) && !t.IsInterface);

            foreach (var validator in validators)
            {
                services.AddTransient(validator);

                var validatorInterfaces = validator
                    .GetInterfaces()
                    .Where(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IValidator<>));

                foreach (var validatorInterface in validatorInterfaces)
                {
                    services.AddTransient(validatorInterface, validator);
                }
            }
        }
    }
}
