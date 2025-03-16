using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class CourseRepositoryBackup : Repository<CourseEntity>, ICourseRepositoryBackup
    {
        private readonly StudentManagementDBContextBackup _dBContext;
        public CourseRepositoryBackup(StudentManagementDBContextBackup dBContext) : base (dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
