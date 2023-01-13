using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Answers;
using TestingSystem.Domain.Entities.Quizes;

namespace TestingSystem.Domain.Entities.Quizes
{
    public class Question : Auditable
    {
        public int NumberOfQuestions { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public int AttachmentId { get; set; }
       
        [MaxLength (200)]
        public string Title { get; set; }
        
        [MaxLength(500)]
        public string Description { get; set; }

        public ICollection<Answer> Answers { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }
}
