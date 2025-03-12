
namespace StudentManagement.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int genericId);
        Task<TEntity> AddAsync(TEntity genericEntity);
        Task<TEntity> UpdateAsync(int genericId, TEntity genericEntity);
        Task<bool> DeleteAsync(int genericId);
    }
}
