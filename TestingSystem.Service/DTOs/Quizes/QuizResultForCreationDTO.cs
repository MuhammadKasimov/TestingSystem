using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizResultForCreationDTO
    {
        [Required]
        public int CorrectAnswers { get; set; }
        [Required]
        public int QuizId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
