using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Entities.Quizes;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class SolvedQuestionForViewDTO
    {
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public bool IsCorrect { get; set; }
        public int QuizResultId { get; set; }
    }
}
