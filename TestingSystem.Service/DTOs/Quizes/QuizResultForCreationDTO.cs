using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Domain.Entities.Users;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizResultForCreationDTO
    {
        public int StartedQuizResultId { get; set; }
        public ICollection<SolvedQuestionForCreationDTO> SolvedQuestions { get; set; }
    }
}
