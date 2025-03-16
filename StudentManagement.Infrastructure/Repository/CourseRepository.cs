using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class CourseRepository : Repository<CourseEntity>, ICourseRepository
    {
        private readonly StudentManagementDBContext _dBcontext;
        public CourseRepository(StudentManagementDBContext dBcontext) : base(dBcontext)
        {
            _dBcontext = dBcontext;
        }
    }
}
