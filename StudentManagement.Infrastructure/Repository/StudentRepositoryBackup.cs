using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class StudentRepositoryBackup : Repository<StudentEntity>, IStudentRepositoryBackup
    {
        private readonly StudentManagementDBContextBackup _dBContext;
        public StudentRepositoryBackup(StudentManagementDBContextBackup dBContext) : base (dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<bool> isEmailUniqueAsync(string email)
        {
            return await _dBContext.Students.AnyAsync(s => s.Email == email);
        }

    }
}
