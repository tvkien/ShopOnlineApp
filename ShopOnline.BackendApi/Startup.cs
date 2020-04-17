using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ShopOnline.Application.Catalogs.Products;
using ShopOnline.Application.Commons;
using ShopOnline.Application.Systems.Users;
using ShopOnline.BackendApi.Extensions;
using ShopOnline.Data.Entities;
using ShopOnline.Data.EntityFramework;
using ShopOnline.Domains;

namespace ShopOnline.BackendApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShopDBContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ShopDatabase")));

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton(provider => Configuration.GetSection("TokensJWT").Get<TokensJWT>());
            services.AddTransient<IFileHandlers>(provider => new FileHandlers(Environment.WebRootPath));
            services.AddTransient<IPublicProductService, PublicProductService>();
            services.AddTransient<IManageProductService, ManageProductService>();
            services.AddTransient<IUsersService, UsersService>();

            services.AddTransient<UserManager<AppUser>, UserManager<AppUser>>();
            services.AddTransient<SignInManager<AppUser>, SignInManager<AppUser>>();
            services.AddIdentity<AppUser, AppRole>()
                    .AddEntityFrameworkStores<ShopDBContext>()
                    .AddDefaultTokenProviders();

            services.AddControllers();
            services.AddSwagger();
            services.AddAuthenticationJwt();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
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