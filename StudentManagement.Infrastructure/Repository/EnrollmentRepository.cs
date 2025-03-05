using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class EnrollmentRepository: IEnrollmentRepository
    {
        private readonly StudentManagementDBContext _dBContext;
        public EnrollmentRepository(StudentManagementDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task<IEnumerable<EnrollmentEntity>> GetEnrollments()
        {
            return await _dBContext.Enrollments.ToListAsync();
        }
        public async Task<EnrollmentEntity> GetEnrollmentByIdAsync(int enrollmentID)
        {
            return await _dBContext.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentID == enrollmentID);
        }
        public async Task<EnrollmentEntity> AddEnrollmentAsync(EnrollmentEntity enrollment)
        {
            _dBContext.Enrollments.Add(enrollment);
            await _dBContext.SaveChangesAsync();
            return enrollment;
        }
        public async Task<EnrollmentEntity> UpdateEnrollmentAsync(int enrollmentID, EnrollmentEntity enrollment)
        {
            var enrollmentToUpdate = await _dBContext.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentID == enrollmentID);
            if (enrollmentToUpdate is not null)
            {
                enrollmentToUpdate.StudentID = enrollment.StudentID;
                enrollmentToUpdate.CourseID = enrollment.CourseID;
                enrollmentToUpdate.EnrollmentDate = enrollment.EnrollmentDate;
                await _dBContext.SaveChangesAsync();
                return enrollmentToUpdate;
            }
            return enrollment;
        }
        public async Task<bool> DeleteEnrollmentAsync(int enrollmentID)
        {
            var enrollmentToDelete = await _dBContext.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentID == enrollmentID);
            if (enrollmentToDelete is not null)
            {
                _dBContext.Enrollments.Remove(enrollmentToDelete);
                return await _dBContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
