using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Application.DTOs
{
    public class CourseReadDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CourseID { get; set; }

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
