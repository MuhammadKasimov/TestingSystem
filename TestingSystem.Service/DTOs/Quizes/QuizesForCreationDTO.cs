using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizForCreationDTO
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
