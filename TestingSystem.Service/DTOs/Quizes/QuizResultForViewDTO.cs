using System.Collections.Generic;
using TestingSystem.Domain.Entities.Quizes;
using TestingSystem.Domain.Entities.Users;
using TestingSystem.Service.DTOs.Users;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizResultForViewDTO
    {
        public int Id { get; set; }
        public int CorrectAnswers { get; set; }
        public Quiz Quiz { get; set; }
        public User User { get; set; }
        public ICollection<SolvedQuestion> SolvedQuestions { get; set; }
    }
}
