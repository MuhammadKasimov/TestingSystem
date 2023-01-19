using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestingSystem.Api.Helpers;
using TestingSystem.Domain.Configurations;
using TestingSystem.Domain.Enums;
using TestingSystem.Service.DTOs.Users;
using TestingSystem.Service.Interfaces.Users;

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

        /// <summary>
        /// Create new user {Admin}
        /// </summary>
        /// <param name="userForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> CreateAsync(UserForCreationDTO userForCreationDTO)
            => Ok(await userService.CreateAsync(userForCreationDTO));

        /// <summary>
        /// Update new user {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userForUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute] int id, UserForUpdateDTO userForUpdateDTO)
            => Ok(await userService.UpdateAsync(id, userForUpdateDTO));
        
        /// <summary>
        /// Change user role {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        [HttpPatch("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> ChangeRoleAsync([FromRoute] int id, UserRole userRole)
           => Ok(await userService.ChangeRoleAsync(id, userRole));
        
        /// <summary>
        /// Change user password {Everyone}
        /// </summary>
        /// <param name="userForChangePasswordDTO"></param>
        /// <returns></returns>
        [HttpPatch("Password"), Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> ChangePasswordAsync(UserForChangePasswordDTO userForChangePasswordDTO)
            => Ok(await userService.ChangePasswordAsync(userForChangePasswordDTO));

        /// <summary>
        /// Get all users {Admin}
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await userService.GetAllAsync(@params));

        /// <summary>
        /// Filtr users by name and degree
        /// </summary>
        /// <param name="params"></param>
        /// <param name="degree"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("filtr"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAllByNameAndDegree([FromQuery] PaginationParams @params, string degree, string name) 
            => Ok(await userService.GetAllByDegreeAndFullNameAsync(@params, degree, name));

       
        /// <summary>
        /// Get all users {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await userService.GetAsync(u => u.Id == id));

        [HttpDelete("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await userService.DeleteAsync(id));
    }
}
