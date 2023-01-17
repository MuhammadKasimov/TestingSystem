using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Entities.Quizes;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class SolvedQuestionForCreationDTO
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
