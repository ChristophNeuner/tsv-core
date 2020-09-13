using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using tsv_core.Models;
using tsv_core.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.HttpOverrides;

namespace tsv_core
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // comment out to run locally (since db is only accessible from the server)
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:tsvIdentity:ConnectionString"]));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseMySql(Configuration["Data:tsvIdentity:ConnectionString"]));
            services.AddDbContext<LoggingDbContext>(options => options.UseMySql(Configuration["Data:tsvRequestLogging:ConnectionString"]));
            services.AddIdentity<AppUser, IdentityRole>(opts => opts.User.RequireUniqueEmail = true).AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddScoped<EFCDatabaseRequestLogger>();
            services.AddSingleton<DBInformation>();
            //



            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions{
                ForwardedHeaders = ForwardedHeaders.XForwardedFor
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug();
            }
            else
            {
                app.UseExceptionHandler("/Shared/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            //This position of the LoggingMiddleware is important!! If it would be placed before "app.UseStaticFiles();" the request paths to the static files would be logged too.
            //If it would be placed behind app.UseMvc, it wouldn't log anything at all.            

            // comment out to run locally (since db is only accessible from the server)
            app.UseMiddleware<LoggingMiddleware>();
            //

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "withId",
                    template: "{controller=Home}/{action=Index}/{Id}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute(
                   name: "SitemapXml",
                   template: "sitemap.xml/{controller=Home}/{action=SitemapXml}");

                routes.MapRoute(
                    name: "robotsText",
                    template: "robots.txt/{controller=Home}/{action=RobotsText}");
            });

            //AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
