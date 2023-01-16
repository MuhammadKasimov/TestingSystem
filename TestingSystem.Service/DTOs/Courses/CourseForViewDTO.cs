using System.Collections.Generic;
using TestingSystem.Service.DTOs.Quizes;

namespace TestingSystem.Service.DTOs.Courses
{
    public class CourseForViewDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<QuizForViewDTO> Quizes { get; set; }
    }
}
