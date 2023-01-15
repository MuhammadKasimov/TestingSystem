using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Service.DTOs.Users;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizResultForViewDTO
    {
        public int CorrectAnswers { get; set; }
        public QuizForViewDTO Quiz { get; set; }
        public UserForViewDTO User { get; set; }
    }
}
