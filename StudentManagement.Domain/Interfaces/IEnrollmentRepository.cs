using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<EnrollmentEntity>> GetEnrollments();
        Task<EnrollmentEntity> GetEnrollmentByIdAsync(int EnrollmentID);
        Task<EnrollmentEntity> AddEnrollmentAsync(EnrollmentEntity enrollment);
        Task<EnrollmentEntity> UpdateEnrollmentAsync(int EnrollmentID, EnrollmentEntity enrollment);
        Task<bool> DeleteEnrollmentAsync(int EnrollmentID);
    }
}
