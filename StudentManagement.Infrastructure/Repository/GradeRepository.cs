using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class GradeRepository: Repository<GradeEntity>, IGradeRepository
    {
        private readonly StudentManagementDBContext _dBcontext;
        public GradeRepository(StudentManagementDBContext dBcontext) : base(dBcontext)
        {
            _dBcontext = dBcontext;
        }
    }
}
