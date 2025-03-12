using StudentManagement.Domain.Entities;

namespace StudentManagement.Domain.Interfaces
{
    public interface IStudentRepository : IRepository<StudentEntity>
    {
        //For FluentValidation
        Task<bool> isEmailUniqueAsync(string email);
    }
}
