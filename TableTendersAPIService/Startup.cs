using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TableTendersAPIService.Models;

namespace TableTendersAPIService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            string con = "Server=(localdb)\\mssqllocaldb;Database=tenderdbstore;Trusted_Connection=True;";
            
            services.AddDbContext<TenderContext>(options => options.UseSqlServer(con));

            services.AddControllers();
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => builder.AllowAnyOrigin());
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors();

            app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseCors();

            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "api",
                    pattern: "api/{controller=Home}/{action=Index}/{id?}")
                .RequireCors("AllowAllOrigin");
            });
        }
    }
}
