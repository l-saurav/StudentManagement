using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly StudentManagementDBContext _dBcontext;
        public CourseRepository(StudentManagementDBContext dBcontext)
        {
            _dBcontext = dBcontext;
        }
        public async Task<IEnumerable<CourseEntity>> GetCourses()
        {
            return await _dBcontext.Courses.ToListAsync();
        }
        public async Task<CourseEntity> GetCourseByIdAsync(int CourseID)
        {
            return await _dBcontext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
        }
        public async Task<CourseEntity> AddCourseAsync(CourseEntity course)
        {
            _dBcontext.Courses.Add(course);
            await _dBcontext.SaveChangesAsync();
            return course;
        }
        public async Task<CourseEntity> UpdateCourseAsync(int CourseID, CourseEntity course)
        {
            var courseToUpdate = await _dBcontext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
            if (courseToUpdate is not null)
            {
                courseToUpdate.CourseName = course.CourseName;
                courseToUpdate.CourseCode = course.CourseCode;
                courseToUpdate.CreditHours = course.CreditHours;
                await _dBcontext.SaveChangesAsync();
                return courseToUpdate;
            }
            return course;
        }
        public async Task<bool> DeleteCourseAsync(int CourseID)
        {
            var courseToDelete = await _dBcontext.Courses.FirstOrDefaultAsync(x => x.CourseID == CourseID);
            if (courseToDelete is not null)
            {
                _dBcontext.Courses.Remove(courseToDelete);
                return await _dBcontext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
