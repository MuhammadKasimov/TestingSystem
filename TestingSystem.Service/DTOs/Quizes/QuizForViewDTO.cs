using System.ComponentModel.DataAnnotations;
using TestingSystem.Domain.Entities.Courses;
using TestingSystem.Service.DTOs.Courses;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizForViewDTO
    {
        public CourseForViewDTO Course { get; set; }
        public int NumberOfQuestions { get; set; }
        public string Title { get; set; }
    }
}
