using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestingSystem.Api.Helpers;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Enums;
using TestingSystem.Service.DTOs.Users;
using TestingSystem.Service.Interfaces;

namespace TestingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
                this.userService = userService;
        }

        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(UserForCreationDTO userForCreationDTO)
        {
            return Ok(await userService.CreateAsync(userForCreationDTO));
        }

        [HttpPut, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync(int id, UserForUpdateDTO userForUpdateDTO)
            => Ok(await userService.UpdateAsync(id, userForUpdateDTO));
        
        [HttpPatch("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> ChangeRoleAsync(int id, UserRole userRole)
           => Ok(await userService.ChangeRoleAsync(id, userRole));
        
        [HttpPatch("Password"), Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO)
            => Ok(await userService.ChangePasswordAsync(userForChangePasswordDTO));

        [HttpGet, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await userService.GetAllAsync(@params));
       

        [HttpGet("{id}/Admin"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await userService.GetAsync(u => u.Id == id));

        [HttpDelete, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await userService.DeleteAsync(id));
    }
}
