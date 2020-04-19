using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ShopOnline.BackendApi.Models;
using AutoMapper;
using ShopOnline.Application.Systems.Users;
using Microsoft.AspNetCore.Authorization;

namespace ShopOnline.BackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IMapper mapper;
        public UsersController(IUsersService usersService, IMapper mapper)
        {
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateAsync([FromBody]LoginRequest request)
        {
            if (!ModelState.IsValid)
            { 
                return BadRequest(ModelState);
            }

            var domainRequest = mapper.Map<Domains.LoginRequest>(request);
            var resultToken = await usersService.AuthenticateAsync(domainRequest);
            
            if (string.IsNullOrEmpty(resultToken))
            {
                return BadRequest("Username or password is incorrect.");
            }

            return Ok(resultToken);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody]RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestService = mapper.Map<Domains.RegisterRequest>(request);
            var errorMessage = await usersService.RegisterAsync(requestService);

            if (string.IsNullOrEmpty(errorMessage))
            {
                return Ok();
            }

            return BadRequest(errorMessage);
        }
    }
}