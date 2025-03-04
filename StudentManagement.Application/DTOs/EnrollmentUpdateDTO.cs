using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
