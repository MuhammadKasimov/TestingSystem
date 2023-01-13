using System.Collections.Generic;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Attachments;

namespace TestingSystem.Domain.Entities.Quizes
{
    public class Question : Auditable
    {
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
