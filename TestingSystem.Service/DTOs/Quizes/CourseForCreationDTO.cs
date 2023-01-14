using System.ComponentModel.DataAnnotations;

namespace TestingSystem.Service.DTOs.Courses
{
    public class CourseForCreationDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
