using TestingSystem.Service.DTOs.Users;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizResultForViewDTO
    {
        public int Id { get; set; }
        public int CorrectAnswers { get; set; }
        public QuizForViewDTO Quiz { get; set; }
        public UserForViewDTO User { get; set; }
    }
}
