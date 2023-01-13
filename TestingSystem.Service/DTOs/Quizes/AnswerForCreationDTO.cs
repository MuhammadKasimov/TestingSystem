using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class AnswerForCreationDTO
    {
        [Required]
        public string Content { get; set; }
        [Required]
        public string Option { get; set; }
        [Required]
        public bool IsCorrect { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
}
