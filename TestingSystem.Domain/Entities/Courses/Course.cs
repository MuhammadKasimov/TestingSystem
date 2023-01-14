using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Quizes;

namespace TestingSystem.Domain.Entities.Courses
{
    public class Course : Auditable
    {
        [MaxLength(64)]
        public string Name { get; set; }
        public ICollection<Quiz> Quizes { get; set; }
    }
}
