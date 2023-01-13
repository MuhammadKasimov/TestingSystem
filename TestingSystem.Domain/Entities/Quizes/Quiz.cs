using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;
namespace TestingSystem.Domain.Entities.Quizes
{
    public class Quiz : Auditable
    {
        public int CourseId { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
