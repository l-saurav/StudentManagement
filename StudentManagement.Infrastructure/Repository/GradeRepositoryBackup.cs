using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class GradeRepositoryBackup: Repository<GradeEntity>, IGradeRepositoryBackup
    {
        private readonly StudentManagementDBContextBackup _dBContext;
        public GradeRepositoryBackup(StudentManagementDBContextBackup dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
