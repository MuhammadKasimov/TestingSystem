using System;
using System.IO;
using System.Threading.Tasks;
using TestingSystem.Data.IRepositories;
using TestingSystem.Domain.Entities.Attachments;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Attachments;
using TestingSystem.Service.Exceptions;
using TestingSystem.Service.Helpers;
using TestingSystem.Service.Interfaces.Quizes;

namespace TestingSystem.Service.Services.Quizes
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IGenericRepository<Attachment> attachmentRepository;
        private readonly IGenericRepository<Question> questionRepository;

        public AttachmentService(IGenericRepository<Attachment> attachmentRepository, IGenericRepository<Question> questionRepository)
        {
            this.attachmentRepository = attachmentRepository;
            this.questionRepository = questionRepository;
        }

        public async ValueTask<Attachment> CreateAsync(int questionId,string fileName, string filePath)
        {
            
            var file = new Attachment()
            {
                Path = filePath
            };
            var question = await questionRepository.GetAsync(q => q.Id == questionId);

            if (question is null)
                throw new TestingSystemException(404, "Question not found");

            file.QuestionId = questionId;
            file = await attachmentRepository.CreateAsync(file);
            await attachmentRepository.SaveChangesAsync();

            return file;
        }

        public async ValueTask<Attachment> UpdateAsync(int id, Stream stream)
        {
            var existAttachment = await attachmentRepository.GetAsync(a => a.Id == id, null);

            if (existAttachment is null)
                throw new TestingSystemException(404, "Attachment not found.");

            string fileName = existAttachment.Path;
            string filePath = Path.Combine(EnvironmentHelper.WebRootPath, fileName);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            await attachmentRepository.SaveChangesAsync();

            return existAttachment;
        }

        public async ValueTask<Attachment> UploadAsync(int questionId,AttachmentForCreationDTO dto)
        {
            string fileName = Guid.NewGuid().ToString("N") + "-" + dto.Name;
            string filePath = Path.Combine(EnvironmentHelper.AttachmentPath, fileName);

            if (!Directory.Exists(EnvironmentHelper.AttachmentPath))
                Directory.CreateDirectory(EnvironmentHelper.AttachmentPath);

            // copy image to the destination as stream
            FileStream fileStream = File.OpenWrite(filePath);
            await dto.Stream.CopyToAsync(fileStream);

            // clear
            await fileStream.FlushAsync();
            fileStream.Close();

            return await CreateAsync(questionId,fileName, Path.Combine(EnvironmentHelper.FilePath, fileName));
        }
    }
}
