using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;


namespace StudentManagement.Infrastructure.Persistence
{
    public class StudentManagementDBContext : DbContext
    {
        public StudentManagementDBContext(DbContextOptions<StudentManagementDBContext> options) : base(options)
        {
        }

        public DbSet<StudentEntity> Students { get; set; }
        public DbSet<CourseEntity> Courses { get; set; }
        public DbSet<EnrollmentEntity> Enrollments { get; set; }
        public DbSet<GradeEntity> Grades { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Student entity
            modelBuilder.Entity<StudentEntity>()
                .HasIndex(s => s.Email)
                .IsUnique();

            modelBuilder.Entity<StudentEntity>()
                .Property(s => s.RegistrationDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<StudentEntity>()
                .Property(s => s.DateOfBirth)
                .HasColumnType("date");

            // Course entity
            modelBuilder.Entity<CourseEntity>()
                .HasIndex(c => c.CourseCode).IsUnique();

            // Enrollment entity
            modelBuilder.Entity<EnrollmentEntity>()
                .HasIndex(e => new { e.StudentID, e.CourseID })
                .IsUnique();

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

            modelBuilder.Entity<EnrollmentEntity>()
                .Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("GETUTCDATE()");

            //Grade entity
            modelBuilder.Entity<GradeEntity>()
                .HasIndex(g => new {g.StudentID,g.CourseID })
                .IsUnique();

            modelBuilder.Entity<GradeEntity>()
                .HasOne(g => g.Student) //One grade belongs to one student
                .WithMany(s => s.Grades) //One student can have multiple grades
                .HasForeignKey(g => g.StudentID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GradeEntity>()
                .HasOne(g => g.Course) //One grade belongs to one course
                .WithMany(c => c.Grades) //One course can have multiple grades
                .HasForeignKey(g => g.CourseID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
