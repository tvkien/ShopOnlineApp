using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopOnline.WebAdmin.BusinessLayers;
using ShopOnline.WebAdmin.Models;
using System;
using System.Threading.Tasks;

namespace ShopOnline.WebAdmin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserHandlers userHandler;
        private readonly IMapper mapper;
        public UsersController(IUserHandlers userHandler, IMapper mapper)
        {
            this.userHandler = userHandler;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

            var domainRequest = mapper.Map<Domains.LoginRequest>(request);
            var token = await userHandler.Authenticate(domainRequest);

            var claimsPrincipal = userHandler.ValidateToken(token);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = false
            };

            //HttpContext.Session.SetString("Token", token);
            await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }
    }
}