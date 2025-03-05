using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentManagementDBContext _dbContext;
        public CourseRepository(StudentManagementDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<CourseEntity>> GetCourses()
        {
            return await _dbContext.Courses.ToListAsync();
        }
        public async Task<CourseEntity> GetCourseByIdAsync(int CourseID)
        {
            return await _dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
        }
        public async Task<CourseEntity> AddCourseAsync(CourseEntity course)
        {
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
            return course;
        }
        public async Task<CourseEntity> UpdateCourseAsync(int CourseID, CourseEntity course)
        {
            var courseToUpdate = await _dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
            if (courseToUpdate is not null)
            {
                courseToUpdate.CourseName = course.CourseName;
                courseToUpdate.CourseCode = course.CourseCode;
                courseToUpdate.CreditHours = course.CreditHours;
                await _dbContext.SaveChangesAsync();
                return courseToUpdate;
            }
            return course;
        }
        public async Task<bool> DeleteCourseAsync(int CourseID)
        {
            var courseToDelete = await _dbContext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
            if (courseToDelete is not null)
            {
                _dbContext.Courses.Remove(courseToDelete);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
