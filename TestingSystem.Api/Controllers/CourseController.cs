using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestingSystem.Api.Helpers;
using TestingSystem.Domain.Configurations;
using TestingSystem.Service.DTOs.Courses;
using TestingSystem.Service.Interfaces;

namespace TestingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        /// <summary>
        /// Create course by {Admin}
        /// </summary>
        /// <param name="courseForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(CourseForCreationDTO courseForCreationDTO)
        {
            return Ok(await courseService.CreateAsync(courseForCreationDTO));
        }

        /// <summary>
        /// Update course by {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseForCreationDTO"></param>
        /// <returns></returns>
        [HttpPut, Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync(int id, CourseForCreationDTO courseForCreationDTO)
           => Ok(await courseService.UpdateAsync(id, courseForCreationDTO));

        /// <summary>
        /// GetAll course by {Admin}
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await courseService.GetAllAsync(@params));

        /// <summary>
        /// Get course {Everyone}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Admin"), Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await courseService.GetAsync(u => u.Id == id));

        /// <summary>
        /// Delete course by {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await courseService.DeleteAsync(id));
    }
}
