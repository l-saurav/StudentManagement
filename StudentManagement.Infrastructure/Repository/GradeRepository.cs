using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class GradeRepository(StudentManagementDBContext context):IGradeRepository
    {
        public async Task<IEnumerable<GradeEntity>> GetGrades()
        {
            return await context.Grades.ToListAsync();
        }
        // Task added to check if the same record exists in the enrollment table
        public async Task<bool> IsStudentEnrolled (int StudentID, int CourseID)
        {
            return await context.Enrollments.AnyAsync( e => e.StudentID ==StudentID && e.CourseID == CourseID);
        }
        public async Task<GradeEntity> GetGradeByIdAsync(int GradeID)
        {
            return await context.Grades.FirstOrDefaultAsync(g => g.GradeID == GradeID);
        }
        public async Task<GradeEntity> AddGradeAsync(GradeEntity grade)
        {
            if(!await IsStudentEnrolled(grade.StudentID,grade.CourseID))
            {
                throw new Exception("Student is not enrolled in the course");
            }
            context.Grades.Add(grade);
            await context.SaveChangesAsync();
            return grade;
        }
        public async Task<GradeEntity> UpdateGradeAsync(int GradeID, GradeEntity grade)
        {
            if(!await IsStudentEnrolled(grade.StudentID, grade.CourseID))
            {
                throw new Exception("Student is not enrolled in the course");
            }
            var gradeToUpdate = await context.Grades.FirstOrDefaultAsync(g => g.GradeID == GradeID);
            if(gradeToUpdate != null)
            {
                gradeToUpdate.StudentID = grade.StudentID;
                gradeToUpdate.CourseID = grade.CourseID;
                gradeToUpdate.Grade = grade.Grade;
                await context.SaveChangesAsync();
                return gradeToUpdate;
            }
            return grade;
        }
        public async Task<bool> DeleteGradeAsync(int GradeID)
        {
            var gradeToDelete = await context.Grades.FirstOrDefaultAsync(g => g.GradeID == GradeID);
            if (gradeToDelete != null)
            {
                context.Grades.Remove(gradeToDelete);
                return await context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
