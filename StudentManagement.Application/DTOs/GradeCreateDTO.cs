using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Application.DTOs
{
    public class GradeCreateDTO
    {
        [Required]
        public int StudentID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        [AllowedValues('A','B','C','D','F')]
        public char Grade { get; set; }
    }
}
