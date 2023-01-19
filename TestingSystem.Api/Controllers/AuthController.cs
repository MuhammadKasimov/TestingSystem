using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TestingSystem.Api.Helpers;
using TestingSystem.Domain.Entities.Users;
using TestingSystem.Service.DTOs.Users;
using TestingSystem.Service.Helpers;
using TestingSystem.Service.Interfaces.Users;

namespace TestingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        /// <summary>
        /// Authorization
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async ValueTask<IActionResult> Login(UserForLoginDTO dto)
        {
            var userSession = new UserSession { UserId = HttpContextHelper.UserId, SessionId = Guid.NewGuid().ToString(), LastActivity = DateTime.Now };
            // store the user session in a database or cache
            //...
            HttpContext.Session.SetString("UserSession", JsonConvert.SerializeObject(userSession));

            var token = await authService.GenerateToken(dto.Login, dto.Password);
            return Ok(new
            {
                token
            });
        }
    }
}
