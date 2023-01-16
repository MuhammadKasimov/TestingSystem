using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestingSystem.Api.Helpers;
using TestingSystem.Domain.Configurations;
using TestingSystem.Service.DTOs.Quizes;
using TestingSystem.Service.Interfaces.Quizes;

namespace TestingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService answerService;
        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService;
        }

        /// <summary>
        /// Create new answer {Admin}
        /// </summary>
        /// <param name="answerForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(AnswerForCreationDTO answerForCreationDTO)
            => Ok(await answerService.CreateAsync(answerForCreationDTO));


        /// <summary>
        /// Update answer {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="answerForCreationDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync(int id, AnswerForCreationDTO answerForCreationDTO)
          => Ok(await answerService.UpdateAsync(id, answerForCreationDTO));

        /// <summary>
        /// Get answer {Everyone}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await answerService.GetAsync(u => u.Id == id));

        /// <summary>
        /// Get all answers {Everyone}
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await answerService.GetAllAsync(@params));

        /// <summary>
        /// Delete answer {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await answerService.DeleteAsync(id));
    }
}
