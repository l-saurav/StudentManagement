using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;

namespace StudentManagement.Infrastructure.Persistence
{
    public class StudentManagementDBContextBackup : DbContext
    {
        public StudentManagementDBContextBackup(DbContextOptions<StudentManagementDBContextBackup> options) : base(options)
        {
        }

        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<EnrollmentEntity> Enrollments { get; set; }
        public DbSet<GradeEntity> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // **Student Entity**
            modelBuilder.Entity<StudentEntity>()
                .Property(s => s.StudentID)
                .ValueGeneratedNever(); // Prevent Identity Auto-Increment

            modelBuilder.Entity<StudentEntity>()
                .Property(s => s.RegistrationDate)
                .IsRequired(); // Ensure it's explicitly provided

            // **Course Entity**
            modelBuilder.Entity<CourseEntity>()
                .Property(c => c.CourseID)
                .ValueGeneratedNever(); // Prevent Identity Auto-Increment

            // **Enrollment Entity**
            modelBuilder.Entity<EnrollmentEntity>()
                .Property(e => e.EnrollmentID)
                .ValueGeneratedNever(); // Prevent Identity Auto-Increment

            modelBuilder.Entity<EnrollmentEntity>()
                .Property(e => e.EnrollmentDate)
                .IsRequired(); // Ensure it's explicitly provided

            // **Grade Entity**
            modelBuilder.Entity<GradeEntity>()
                .Property(g => g.GradeID)
                .ValueGeneratedNever(); // Prevent Identity Auto-Increment

            // **Foreign Key Relationships Remain Unchanged**
            modelBuilder.Entity<EnrollmentEntity>()
                .HasOne(e => e.Student)
                .WithMany(s => s.Enrollments)
                .HasForeignKey(e => e.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EnrollmentEntity>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Enrollments)
                .HasForeignKey(e => e.CourseID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GradeEntity>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GradeEntity>()
                .HasOne(g => g.Course)
                .WithMany(c => c.Grades)
                .HasForeignKey(g => g.CourseID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
