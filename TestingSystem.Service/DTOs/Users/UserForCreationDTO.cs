using System.ComponentModel.DataAnnotations;
using TestingSystem.Service.Attributes;

namespace TestingSystem.Service.DTOs.Users
{
    public class UserForCreationDTO
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [UserLogin, Required]
        public string Username { get; set; }

        [UserPassword, Required]
        public string Password { get; set; }
        [Required]
        public string Degree { get; set; }

    }
}