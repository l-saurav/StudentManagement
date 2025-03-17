using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Domain.Entities
{
    public class StudentEntity
    {
        [Key] // Primary key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto increment
        public int StudentID { get; set; }

        [Required(ErrorMessage ="Name field cannot be left empty!")]
        [MaxLength(50, ErrorMessage ="Name cannot be more than 50 character long!")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Date of Birth field cannot be left empty!")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage ="Email field cannot be left empty!")]
        [EmailAddress(ErrorMessage ="Invalid email address provided!")]
        [MaxLength (50,ErrorMessage ="Email Address cannot contain more than 50 character")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone Number cannot be left empty!")]
        [MaxLength(10, ErrorMessage ="Phone Number cannot be more than 10 digit")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Registration Date cannot be left empty!")]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Navigation property
        public ICollection<EnrollmentEntity> Enrollments { get; set; }
        public ICollection<GradeEntity> Grades { get; set; }
    }
}
