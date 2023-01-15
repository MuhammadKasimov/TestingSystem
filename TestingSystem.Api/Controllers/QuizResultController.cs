using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class QuizResultController : ControllerBase
    {
        private readonly IQuizResultService quizResultService;
        public QuizResultController(IQuizResultService quizResultService)
        {
            this.quizResultService = quizResultService;
        }

        /// <summary>
        /// Create new quiz result {Admin}
        /// </summary>
        /// <param name="courseForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(QuizResultForCreationDTO courseForCreationDTO)
            => Ok(await quizResultService.CreateAsync(courseForCreationDTO));

        /// <summary>
        /// Get a quiz result {Everyone}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}/Admin"), Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
            => Ok(await quizResultService.GetAsync(u => u.Id == id));

        /// <summary>
        /// Get all quiz results {Everyone}
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await quizResultService.GetAllAsync(@params));
    }
}
