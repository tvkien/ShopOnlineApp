using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using ShopOnline.Domains;
using ShopOnline.WebAdmin.BusinessLayers;
using ShopOnline.WebAdmin.FluentValidations;
using ShopOnline.WebAdmin.ServiceLayers;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace ShopOnline.WebAdmin
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient(HttpClientName.BackendApi, client=> {
                client.BaseAddress = new Uri("http://localhost:5000");
            });

            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton(provider => Configuration.GetSection("TokensJWT").Get<TokensJWT>());

            services.AddTransient<IUserApiClient, UserApiClient>();
            services.AddTransient<ISecurityTokenValidator, JwtSecurityTokenHandler>();
            services.AddTransient<IUserHandlers, UserHandlers>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Users/Login/";
                    options.LogoutPath = "/Users/Logout/";
                    options.AccessDeniedPath = "/Users/Login";
                    options.Cookie.SameSite = SameSiteMode.Strict;
                    options.Cookie.HttpOnly = true;
                });

            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromSeconds(10);
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;
            //});

            services.AddControllersWithViews()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseHttpContextItemsMiddleware();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}