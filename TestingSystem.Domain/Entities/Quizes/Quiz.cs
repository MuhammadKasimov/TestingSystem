using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Courses;

namespace TestingSystem.Domain.Entities.Quizes
{
    public class Quiz : Auditable
    {
        public int NumberOfQuestions { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
