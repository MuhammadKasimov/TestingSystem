using Microsoft.AspNetCore.Http;
using System.IO;
using TestingSystem.Service.DTOs.Attachments;

namespace TestingSystem.Api.Extensions
{
    public static class FormFIleExtensions
    {
        public static AttachmentForCreationDTO ToAttachmentOrDefault(this IFormFile formFile)
        {

            if (formFile?.Length > 0)
            {
                using var ms = new MemoryStream();
                formFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                return new AttachmentForCreationDTO()
                {
                    Name = formFile.FileName,
                    Stream = new MemoryStream(fileBytes)
                };
            }

            return null;
        }
    }
}
