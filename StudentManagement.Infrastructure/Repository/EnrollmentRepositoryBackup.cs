using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Infrastructure.Repository
{
    public class EnrollmentRepositoryBackup : Repository<EnrollmentEntity>, IEnrollmentRepositoryBackup
    {
        private readonly StudentManagementDBContextBackup _dBContext;
        public EnrollmentRepositoryBackup(StudentManagementDBContextBackup dBContext) : base(dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
