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
    public class QuizController : ControllerBase
    {
        private readonly IQuizService quizService;

        public QuizController(IQuizService quizService)
        {
            this.quizService = quizService;
        }

        /// <summary>
        /// Create new quiz {Admin}
        /// </summary>
        /// <param name="quizForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(QuizForCreationDTO quizForCreationDTO)
            => Ok(await quizService.CreateAsync(quizForCreationDTO));

        /// <summary>
        /// Update a quiz {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quizForUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync(int id, QuizForCreationDTO quizForUpdateDTO)
            => Ok(await quizService.UpdateAsync(id, quizForUpdateDTO));

        /// <summary>
        /// Get all quizzes {Everyone}
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await quizService.GetAllAsync(@params));

        /// <summary>
        /// Get a quiz {Everyone}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await quizService.GetAsync(u => u.Id == id));



        /// <summary>
        /// Delete a quiz {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await quizService.DeleteAsync(id));
    }
}
