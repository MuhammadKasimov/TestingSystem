using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Users
{
    public class UserForLoginDTO
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
