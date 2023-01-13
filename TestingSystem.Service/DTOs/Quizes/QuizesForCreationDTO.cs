using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizesForCreationDTO
    {
        [Required]
        public int CourseId { get; set; }
        [Required]
        public int NumberOfQuestion { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
