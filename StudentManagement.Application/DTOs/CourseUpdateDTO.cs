using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs
{
    public class CourseUpdateDTO
    {

        [Required]
        [MaxLength(30)]
        public string CourseName { get; set; }

        [Required]
        [MaxLength(10)]
        public string CourseCode { get; set; }

        [Required]
        [Range(1, 5)]
        public int CreditHours { get; set; }
    }
}
