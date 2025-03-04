

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Application.DTOs
{
    public class GradeUpdateDTO
    {
        [Required]
        public int StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        [AllowedValues('A', 'B', 'C', 'D', 'F')]
        public char Grade { get; set; }
    }
}
