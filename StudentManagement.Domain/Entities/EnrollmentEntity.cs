
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Domain.Entities
{
    public class EnrollmentEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EnrollmentID { get; set; }

        [Required]
        public int StudentID { get; set; }
        [Required]
        public int CourseID { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        //Define Foreign Key Relationships
        [ForeignKey("StudentID")]
        public StudentEntity Student { get; set; }
        [ForeignKey("CourseID")]
        public CourseEntity Course { get; set; }
    }
}
