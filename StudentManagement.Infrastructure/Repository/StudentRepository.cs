using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Infrastructure.Repository
{
    public class StudentRepository : Repository<StudentEntity>, IStudentRepository
    {
        private readonly StudentManagementDBContext _dBContext;
        public StudentRepository(StudentManagementDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }

        // For Fluent Validation
        public async Task<bool> isEmailUniqueAsync(string email)
        {
            return !await _dBContext.Students.AnyAsync(x => x.Email == email);
        }
        //
    }
}
