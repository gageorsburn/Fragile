using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Entity;
using Fragile.Models;
using Microsoft.Extensions.Configuration;

namespace Fragile
{
    public class Startup
    {

        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer("Server=tcp:fragile.database.windows.net,1433;Database=fragiledb;User ID=fragile@fragile;Password=Drew1022807!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
                    */

            var builder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                //All environment variables in the process's context flow in as configuration values.
                .AddEnvironmentVariables();

            Configuration = builder.Build();

            services.AddEntityFramework().AddSqlServer().AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();
            app.UseStaticFiles();

            app.UseMvc(mvc => mvc.MapRoute(
                name: "default",
                template: "{controller}/{action}/{id?}",
                defaults: new { controller="Services", action="Index", }
                ));

            app.UseDeveloperExceptionPage();
            app.UseStatusCodePages();
        }

        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
