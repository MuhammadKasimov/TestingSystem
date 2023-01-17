using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;

namespace TestingSystem.Domain.Entities.Quizes
{
    public class SolvedQuestion : Auditable
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizResultId { get; set; }
    }
}
