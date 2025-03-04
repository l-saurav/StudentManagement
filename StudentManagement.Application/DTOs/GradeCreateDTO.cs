using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
