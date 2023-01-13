using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Quizes;

namespace TestingSystem.Domain.Entities.Answers
{
    public class Answer : Auditable
    {
        [MaxLength (400)]
        public string Content { get; set; }

        [MaxLength (1)]
        public string Option { get; set; }
        public bool IsCorrect { get; set; }

        public Question Questions { get; set; }
    }
}
