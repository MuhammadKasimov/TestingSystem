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
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService answerService;
        public AnswerController(IAnswerService answerService)
        {
            this.answerService = answerService; 
        }

        [HttpPost, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async ValueTask<IActionResult> CreateAsync(AnswerForCreationDTO answerForCreationDTO)
            => Ok(await answerService.CreateAsync(answerForCreationDTO));

        [HttpPut, Authorize(Roles = CustomRoles.USER_ROLE)]
        public async ValueTask<IActionResult> UpdateAsync(int id, AnswerForCreationDTO answerForCreationDTO)
          => Ok(await answerService.UpdateAsync(id, answerForCreationDTO));

        [HttpGet("{id}/Admin"), Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAsync([FromRoute] int id)
           => Ok(await answerService.GetAsync(u => u.Id == id));

        [HttpGet, Authorize(Roles = CustomRoles.ALL_ROLES)]
        public async ValueTask<IActionResult> GetAll([FromQuery] PaginationParams @params)
           => Ok(await answerService.GetAllAsync(@params));

        [HttpDelete, Authorize(Roles = CustomRoles.ADMIN_ROLE)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
            => Ok(await answerService.DeleteAsync(id));
    }
}
