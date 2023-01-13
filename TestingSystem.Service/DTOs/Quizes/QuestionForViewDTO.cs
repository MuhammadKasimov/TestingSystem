using System.Collections.Generic;
using TestingSystem.Service.DTOs.Attachments;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuestionForViewDTO
    {
        public int QuizId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public ICollection<AnswerForViewDTO> Answers { get; set; }
        public ICollection<AttachmentForViewDTO> Attachments { get; set; }
    }
}