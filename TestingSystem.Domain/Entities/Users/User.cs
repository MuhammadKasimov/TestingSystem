using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingSystem.Domain.Commons;
using TestingSystem.Domain.Enums;

namespace TestingSystem.Domain.Entities.Users
{
    public class User : Auditable
    {
        [MaxLength(32)]
        public string FirstName { get; set; }

        [MaxLength(32)]
        public string LastName { get; set; }

        [MaxLength(64)]
        public string Email { get; set; }

        [MaxLength(128)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string Username { get; set; }
        public string IpAddress { get; set; }
        public string Degree { get; set; }
        public UserRole Role { get; set; }
    }
}
