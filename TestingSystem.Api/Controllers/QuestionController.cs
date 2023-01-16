using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestingSystem.Api.Helpers;
using TestingSystem.Domain.Configurations;
using TestingSystem.Service.DTOs.Quizes;
using TestingSystem.Service.Interfaces.Questiones;

namespace TestingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService questionService;
        public QuestionController(IQuestionService questionService)
        {
            this.questionService = questionService;
        }

        /// <summary>
        /// Create new question {Admin)
        /// </summary>
        /// <param name="questionForCreationDTO"></param>
        /// <returns></returns>
        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
            => Ok(await questionService.CreateAsync(questionForCreationDTO));

        [HttpPut("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute] int id, QuestionForCreationDTO questionForCreationDTO)
          => Ok(await questionService.UpdateAsync(id, questionForCreationDTO));

        /// <summary>
        /// Get all questions {Everyone}
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet, Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await questionService.GetAllAsync(@params));

     
        /// <summary>
        /// Get a question {Everyone}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}"), Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await questionService.GetAsync(u => u.Id == id));


        /// <summary>
        /// Delete a question {Admin}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}"), Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await questionService.DeleteAsync(id));
    }
}
