using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IStudentRepositoryBackup : IRepository<StudentEntity>
    {
        //For FluentValidation
        Task<bool> isEmailUniqueAsync(string email);
    }
}
