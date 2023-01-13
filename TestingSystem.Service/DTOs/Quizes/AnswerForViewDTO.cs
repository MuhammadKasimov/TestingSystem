using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class AnswerForViewDTO
    {
        public string Content { get; set; }
        public string Option { get; set; }
        public bool IsCorrect { get; set; }
        public QuestionForViewDTO Question { get; set; }
    }
}
