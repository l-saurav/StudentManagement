using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs
{
    public class EnrollmentUpdateDTO
    {

        [Required]
        public int StudentID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
    }
}
