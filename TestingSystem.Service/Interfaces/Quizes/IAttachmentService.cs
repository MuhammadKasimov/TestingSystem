using System.IO;
using System.Threading.Tasks;
using TestingSystem.Domain.Entities.Attachments;
using TestingSystem.Service.DTOs.Attachments;

namespace TestingSystem.Service.Interfaces.Quizes
{
    public interface IAttachmentService
    {
        ValueTask<Attachment> UploadAsync( int questionId,AttachmentForCreationDTO dto);
        ValueTask<Attachment> UpdateAsync(int id, Stream stream);
        ValueTask<Attachment> CreateAsync(int questionId,string fileName, string filePath);
    }
}
