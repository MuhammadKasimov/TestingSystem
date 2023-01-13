using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuestionForCreationDTO
    {
        [Required]
        public int QuizId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }

    }
}
