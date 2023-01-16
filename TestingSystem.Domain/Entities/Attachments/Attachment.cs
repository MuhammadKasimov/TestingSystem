using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Quizes;

namespace TestingSystem.Domain.Entities.Attachments
{
    public class Attachment : Auditable
    {
        public string Path { get; set; }

        public int QuestionId { get; set; }
    }
}
