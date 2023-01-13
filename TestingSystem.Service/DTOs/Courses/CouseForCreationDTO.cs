using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Courses
{
    public class CouseForCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
