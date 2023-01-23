using System.Collections.Generic;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Service.DTOs.Courses;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizForViewDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int NumberOfQuestions { get; set; }
        public string Title { get; set; }
        public int TimeToSolveInMinutes { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
