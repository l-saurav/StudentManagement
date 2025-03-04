using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class CourseRepository(StudentManagementDBContext dbContext) : ICourseRepository
    {
        public async Task<IEnumerable<CourseEntity>> GetCourses()
        {
            return await dbContext.Courses.ToListAsync();
        }
        public async Task<CourseEntity> GetCourseByIdAsync(int CourseID)
        {
            return await dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
        }
        public async Task<CourseEntity> AddCourseAsync(CourseEntity course)
        {
            dbContext.Courses.Add(course);
            await dbContext.SaveChangesAsync();
            return course;
        }
        public async Task<CourseEntity> UpdateCourseAsync(int CourseID, CourseEntity course)
        {
            var courseToUpdate = await dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
            if (courseToUpdate is not null)
            {
                courseToUpdate.CourseName = course.CourseName;
                courseToUpdate.CourseCode = course.CourseCode;
                courseToUpdate.CreditHours = course.CreditHours;
                await dbContext.SaveChangesAsync();
                return courseToUpdate;
            }
            return course;
        }
        public async Task<bool> DeleteCourseAsync(int CourseID)
        {
            var courseToDelete = await dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
            if (courseToDelete is not null)
            {
                dbContext.Courses.Remove(courseToDelete);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
