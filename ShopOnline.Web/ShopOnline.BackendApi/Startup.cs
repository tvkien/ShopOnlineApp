using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ShopOnline.Application.Catalogs.Products;
using ShopOnline.Data.EntityFramework;

namespace ShopOnline.BackendApi
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
            services.AddDbContext<ShopDBContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ShopDatabase")));

            services.AddTransient<IPublicProductService, PublicProductService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Shop Online Backend API",
                    Version = "v1" 
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Shop Online Backend API");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("ping", async (context) =>
                {
                    await context.Response.WriteAsync("pong");
                });
                endpoints.MapControllers();
            });
        }
    }
}