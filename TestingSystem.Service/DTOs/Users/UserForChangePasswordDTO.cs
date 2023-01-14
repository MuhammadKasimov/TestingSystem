using System.ComponentModel.DataAnnotations;
using TestingSystem.Service.Attributes;

namespace TestingSystem.Service.DTOs.Users
{
    public class UserForChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; }
        [Required, UserPassword]
        public string NewPassword { get; set; }
    }
}
