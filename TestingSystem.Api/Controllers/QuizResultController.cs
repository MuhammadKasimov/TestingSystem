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
    public class QuizResultController : ControllerBase
    {
        private readonly IQuizResultService quizResultService;
        public QuizResultController(IQuizResultService quizResultService)
        {
            this.quizResultService = quizResultService;
        }


        [HttpPost, Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(QuizResultForCreationDTO courseForCreationDTO)
            => Ok(await quizResultService.CreateAsync(courseForCreationDTO));

        /// <summary>
        /// Get a quiz result {Everyone}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = CustomRoles.ALL_ROLES)]
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

        [HttpGet("{quizId}/Excel"), Authorize(Roles = CustomRoles.TEACHER_ROLE)]
        public async ValueTask<IActionResult> GetAllInExcel([FromRoute] int quizId)
            => Ok(await quizResultService.GetAllInExcel(quizId));
    }
}
