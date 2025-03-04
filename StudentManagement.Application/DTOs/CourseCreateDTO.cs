using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.DTOs
{
    public class CourseCreateDTO
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
