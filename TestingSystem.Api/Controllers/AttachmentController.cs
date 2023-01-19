using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestingSystem.Api.Extensions;
using TestingSystem.Api.Helpers;
using TestingSystem.Service.Interfaces.Quizes;

namespace TestingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = CustomRoles.ADMIN_ROLE)]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            this.attachmentService = attachmentService;
        }

        [HttpPost("{questionId}")]
        public async ValueTask<IActionResult> UploadAsync([FromRoute] int questionId,IFormFile formFile)
        {
            return Ok(await attachmentService.UploadAsync(questionId,formFile.ToAttachmentOrDefault()));
        }

        [HttpPut("{id}")]
        public async ValueTask<IActionResult> UpdateAsync([FromRoute] int id, IFormFile formFile)
        {
            return Ok(await attachmentService.UpdateAsync(id, formFile.ToAttachmentOrDefault().Stream));
        }
    }
}
