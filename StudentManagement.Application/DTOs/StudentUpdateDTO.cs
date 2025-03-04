using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Application.DTOs
{
    public class StudentUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; } // Making this optional for updates

        [Required]
        [EmailAddress]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }
    }
}
