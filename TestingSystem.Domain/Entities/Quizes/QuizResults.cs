using System.Collections.Generic;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Users;

namespace TestingSystem.Domain.Entities.Quizes
{
    public class QuizResult : Auditable
    {
        public int CorrectAnswers { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<SolvedQuestion> SolvedQuestions { get; set; }
    }
}
