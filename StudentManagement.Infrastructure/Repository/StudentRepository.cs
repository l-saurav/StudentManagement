using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Infrastructure.Repository
{
    public class StudentRepository (StudentManagementDBContext dbContext) : IStudentRepository
    {
        // For Fluent Validation
        public async Task<bool> isEmailUniqueAsync(string email)
        {
            return await dbContext.Students.AnyAsync(x => x.Email != email);
        }

        public async Task<IEnumerable<StudentEntity>> GetStudents()
        {
            return await dbContext.Students.ToListAsync();
        }

        public async Task<StudentEntity> GetStudentByIdAsync(int StudentID)
        {
            return await dbContext.Students.FirstOrDefaultAsync(x => x.StudentID == StudentID);
        }

        public async Task<StudentEntity> AddStudentAsync(StudentEntity student)
        {
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
            return student;
        }

        public async Task<StudentEntity> UpdateStudentAsync(int StudentID, StudentEntity student)
        {
            var studentToUpdate = await dbContext.Students.FirstOrDefaultAsync(x => x.StudentID == StudentID);
            if(studentToUpdate is not null) {
                studentToUpdate.FullName = student.FullName;
                studentToUpdate.DateOfBirth = student.DateOfBirth;
                studentToUpdate.Email = student.Email;
                studentToUpdate.PhoneNumber = student.PhoneNumber;
                await dbContext.SaveChangesAsync();
                return studentToUpdate;
            }
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int StudentID)
        {
            var studentToDelete = await dbContext.Students.FirstOrDefaultAsync(x => x.StudentID == StudentID);
            if (studentToDelete is not null)
            {
                dbContext.Students.Remove(studentToDelete);
                return await dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
