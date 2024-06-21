using Marvel.Application.Repositories;
using Marvel.Infrastruture.Context;

namespace Marvel.Infrastruture.Repositories
{
    public class Repository(ApplicationDBContext context) : IRepository
    {
        private readonly ApplicationDBContext _context = context;
        public async Task AddAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(int id) where T : class
        {
            var data = await _context.Set<T>().FindAsync(id);
            if (data != null)
            {
                _context.Set<T>().Remove(data);
                _context.SaveChanges();
            }
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return [.. _context.Set<T>()];
        }

        public async Task<T?> GetByIdAsync<T>(Guid id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T?> GetByIdAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
