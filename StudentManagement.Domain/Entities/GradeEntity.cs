
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Domain.Entities
{
    public class GradeEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GradeID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        [AllowedValues('A', 'B', 'C', 'D', 'F')]
        public char Grade { get; set; }

        //Define Foreign Key Relationships
        [ForeignKey("StudentID")]
        public StudentEntity Student { get; set; }
        [ForeignKey("CourseID")]
        public CourseEntity Course { get; set; }

    }
}
