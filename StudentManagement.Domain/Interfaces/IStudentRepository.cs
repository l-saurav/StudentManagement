using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentEntity>> GetStudents();
        Task<StudentEntity> GetStudentByIdAsync(int StudentID);
        Task<StudentEntity> AddStudentAsync(StudentEntity student);
        Task<StudentEntity> UpdateStudentAsync(int StudentID, StudentEntity student);
        Task<bool> DeleteStudentAsync(int StudentID);

        //For FluentValidation
        Task<bool> isEmailUniqueAsync(string email);
    }
}
