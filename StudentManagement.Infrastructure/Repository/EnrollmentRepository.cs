using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Domain.Interfaces;
using StudentManagement.Infrastructure.Persistence;

namespace StudentManagement.Infrastructure.Repository
{
    public class EnrollmentRepository(StudentManagementDBContext dBContext): IEnrollmentRepository
    {
        public async Task<IEnumerable<EnrollmentEntity>> GetEnrollments()
        {
            return await dBContext.Enrollments.ToListAsync();
        }
        public async Task<EnrollmentEntity> GetEnrollmentByIdAsync(int enrollmentID)
        {
            return await dBContext.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentID == enrollmentID);
        }
        public async Task<EnrollmentEntity> AddEnrollmentAsync(EnrollmentEntity enrollment)
        {
            dBContext.Enrollments.Add(enrollment);
            await dBContext.SaveChangesAsync();
            return enrollment;
        }
        public async Task<EnrollmentEntity> UpdateEnrollmentAsync(int enrollmentID, EnrollmentEntity enrollment)
        {
            var enrollmentToUpdate = await dBContext.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentID == enrollmentID);
            if (enrollmentToUpdate is not null)
            {
                enrollmentToUpdate.StudentID = enrollment.StudentID;
                enrollmentToUpdate.CourseID = enrollment.CourseID;
                enrollmentToUpdate.EnrollmentDate = enrollment.EnrollmentDate;
                await dBContext.SaveChangesAsync();
                return enrollmentToUpdate;
            }
            return enrollment;
        }
        public async Task<bool> DeleteEnrollmentAsync(int enrollmentID)
        {
            var enrollmentToDelete = await dBContext.Enrollments.FirstOrDefaultAsync(x => x.EnrollmentID == enrollmentID);
            if (enrollmentToDelete is not null)
            {
                dBContext.Enrollments.Remove(enrollmentToDelete);
                return await dBContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
