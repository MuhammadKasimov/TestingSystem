using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Enums;

namespace TestingSystem.Domain.Entities.Users
{
    public class User : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string IpAddress { get; set; }
        public string Degree { get; set; }
        public UserRole Role { get; set; }
    }
}
