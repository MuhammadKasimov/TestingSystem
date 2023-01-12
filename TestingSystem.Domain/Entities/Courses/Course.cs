using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;

namespace TestingSystem.Domain.Entities.Courses
{
    public class Course : Auditable
    {
        public string Name { get; set; }
    }
}
