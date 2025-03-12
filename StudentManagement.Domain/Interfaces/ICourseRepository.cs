using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<CourseEntity>> GetCourses();
        Task<CourseEntity> GetCourseByIdAsync(int CourseID);
        Task<CourseEntity> AddCourseAsync(CourseEntity course);
        Task<CourseEntity> UpdateCourseAsync(int CourseID, CourseEntity course);
        Task<bool> DeleteCourseAsync(int CourseID);
    }
}
