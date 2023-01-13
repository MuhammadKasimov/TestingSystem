using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Users
{
    public class UserForChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
