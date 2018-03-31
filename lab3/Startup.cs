using DbDataLibrary.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using lab3.Utils;
using Microsoft.AspNetCore.Mvc;
using lab3.Middleware;

namespace lab3
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
            ////add database connection
            //string connectionString = Configuration.GetConnectionString("SqliteConnection");
            //services.AddDbContext<IToursDbContext<ToursSqliteDbContext>>(
            //    options => options.UseSqlite(connectionString)
            //);
            services.AddDbContext<ToursSqliteDbContext>();

            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add(Constants.CachedProfile,
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.Any,
                        Duration = Constants.Seconds
                    });
                options.CacheProfiles.Add("NoCaching",
                    new CacheProfile()
                    {
                        Location = ResponseCacheLocation.None,
                        NoStore = true
                    });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();
            app.UseOperatinCache(Constants.DbTablesCache);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Main}/{action=Index}/{id?}");
            });
        }
    }
}
