using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Questions;

namespace TestingSystem.Domain.Entities.Answers
{
    public class Answer : Auditable
    {
        public string Content { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }

        public Question Questions { get; set; }
    }
}
