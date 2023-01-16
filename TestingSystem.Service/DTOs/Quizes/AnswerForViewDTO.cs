using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class AnswerForViewDTO
    {
        [MaxLength(400)]
        public string Content { get; set; }

        [MaxLength(1)]
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
    }
}
