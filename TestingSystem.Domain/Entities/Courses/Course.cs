using System.ComponentModel.DataAnnotations;
using TestingSystem.Domain.Commons;

namespace TestingSystem.Domain.Entities.Courses
{
    public class Course : Auditable
    {
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
