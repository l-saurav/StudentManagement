namespace StudentManagement.Domain.Interfaces
{
    public interface ISynchronizationService
    {
        Task SynchronizeStudentsAsync();
        Task SynchronizeCoursesAsync();
    }
}
