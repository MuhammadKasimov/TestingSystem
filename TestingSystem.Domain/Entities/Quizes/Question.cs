using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;

namespace TestingSystem.Domain.Entities.Quizes
{
    public class Question : Auditable
    {
        public int NumberOfQuestions { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }

        public int AttachmentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
