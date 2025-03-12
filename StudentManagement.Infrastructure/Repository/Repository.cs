using Microsoft.EntityFrameworkCore;
using StudentManagement.Domain.Interfaces;


namespace StudentManagement.Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> 
        where TEntity : class
    {
        private readonly DbContext _dBContext;
        private readonly DbSet<TEntity> _dBSet;
        public Repository(DbContext dBContext)
        {
            _dBContext = dBContext;
            _dBSet = _dBContext.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity genericEntity)
        {
            _dBSet.Add(genericEntity);
            await _dBContext.SaveChangesAsync();
            return genericEntity;
        }

        public async Task<bool> DeleteAsync(int genericId)
        {
            var entityToDelete = await _dBSet.FindAsync(genericId);
            if(entityToDelete is not null)
            {
                _dBSet.Remove(entityToDelete);
                return await _dBContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dBSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int genericId)
        {
            return await _dBSet.FindAsync(genericId);
        }

        public async Task<TEntity> UpdateAsync(int genericId, TEntity genericEntity)
        {
            var entityToUpdate = await _dBSet.FindAsync(genericId);
            if (entityToUpdate is not null)
            {
                _dBContext.Entry(entityToUpdate).CurrentValues.SetValues(genericEntity);
                await _dBContext.SaveChangesAsync();
                return entityToUpdate;
            }
            return genericEntity;
        }
    }
}
