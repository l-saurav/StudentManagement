using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class EnrollmentRepository: Repository<EnrollmentEntity>, IEnrollmentRepository
    {
        private readonly StudentManagementDBContext _dBContext;
        public EnrollmentRepository(StudentManagementDBContext dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
