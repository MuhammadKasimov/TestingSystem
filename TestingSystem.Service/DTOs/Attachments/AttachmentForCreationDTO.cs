using System.IO;

namespace TestingSystem.Service.DTOs.Attachments
{
    public class AttachmentForCreationDTO
    {
        public string Name { get; set; }
        public int QuiestionId { get; set; }
        public Stream Stream { get; set; }
    }
}
