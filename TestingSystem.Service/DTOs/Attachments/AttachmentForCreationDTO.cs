using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingSystem.Service.DTOs.Attachments
{
    public class AttachmentForCreationDTO
    {
        public string Name { get; set; }
        public int QuiestionId { get; set; }
        public Stream Stream { get; set; }
    }
}
