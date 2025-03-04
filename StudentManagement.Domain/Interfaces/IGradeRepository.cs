using StudentManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Domain.Interfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<GradeEntity>> GetGrades();
        Task<GradeEntity> GetGradeByIdAsync(int GradeID);
        Task<GradeEntity> AddGradeAsync(GradeEntity grade);
        Task<GradeEntity> UpdateGradeAsync(int GradeID, GradeEntity grade);
        Task<bool> DeleteGradeAsync (int GradeID);
    }
}
