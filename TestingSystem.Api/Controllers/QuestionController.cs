using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(QuestionForCreationDTO questionForCreationDTO)
            => Ok(await questionService.CreateAsync(questionForCreationDTO));

        [HttpPut, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync(int id, QuestionForCreationDTO questionForCreationDTO)
          => Ok(await questionService.UpdateAsync(id, questionForCreationDTO));

        [HttpGet, Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await questionService.GetAllAsync(@params));

        [HttpGet("{id}/Admin"), Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await questionService.GetAsync(u => u.Id == id));

        [HttpDelete, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await questionService.DeleteAsync(id));
    }
}
