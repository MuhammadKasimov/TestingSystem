using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Entities.Quizes;

namespace TestingSystem.Domain.Entities.Attachments
{
    public class Attachment : Auditable
    {
        public string Path { get; set; }

        public Question Questions { get; set; }
    }
}
