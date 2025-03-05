using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Entities;
using StudentManagement.Infrastructure.Persistence;
using StudentManagement.Domain.Interfaces;

namespace StudentManagement.Infrastructure.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentManagementDBContext _dBContext;
        public StudentRepository(StudentManagementDBContext dBContext)
        {
            _dBContext = dBContext;
        }

        // For Fluent Validation
        public async Task<bool> isEmailUniqueAsync(string email)
        {
            return await _dBContext.Students.AnyAsync(x => x.Email != email);
        }
        //

        public async Task<IEnumerable<StudentEntity>> GetStudents()
        {
            return await _dBContext.Students.ToListAsync();
        }

        public async Task<StudentEntity> GetStudentByIdAsync(int StudentID)
        {
            return await _dBContext.Students.FirstOrDefaultAsync(x => x.StudentID == StudentID);
        }

        public async Task<StudentEntity> AddStudentAsync(StudentEntity student)
        {
            _dBContext.Students.Add(student);
            await _dBContext.SaveChangesAsync();
            return student;
        }

        public async Task<StudentEntity> UpdateStudentAsync(int StudentID, StudentEntity student)
        {
            var studentToUpdate = await _dBContext.Students.FirstOrDefaultAsync(x => x.StudentID == StudentID);
            if(studentToUpdate is not null) {
                studentToUpdate.FullName = student.FullName;
                studentToUpdate.DateOfBirth = student.DateOfBirth;
                studentToUpdate.Email = student.Email;
                studentToUpdate.PhoneNumber = student.PhoneNumber;
                await _dBContext.SaveChangesAsync();
                return studentToUpdate;
            }
            return student;
        }

        public async Task<bool> DeleteStudentAsync(int StudentID)
        {
            var studentToDelete = await _dBContext.Students.FirstOrDefaultAsync(x => x.StudentID == StudentID);
            if (studentToDelete is not null)
            {
                _dBContext.Students.Remove(studentToDelete);
                return await _dBContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
