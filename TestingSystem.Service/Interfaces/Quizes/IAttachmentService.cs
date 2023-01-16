using System.IO;
using System.Threading.Tasks;
using TestingSystem.Domain.Entities.Attachments;
using TestingSystem.Service.DTOs.Attachments;

namespace TestingSystem.Service.Interfaces.Quizes
{
    public interface IAttachmentService
    {
        ValueTask<Attachment> UploadAsync(AttachmentForCreationDTO dto);
        ValueTask<Attachment> UpdateAsync(int id, Stream stream);
        ValueTask<Attachment> CreateAsync(string fileName, string filePath);
    }
}
