using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Service.DTOs.Quizes
{
    public class QuizResultForViewInExcelDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CorrectAnswers { get; set; }

    }
}
