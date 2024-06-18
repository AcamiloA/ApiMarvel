using Marvel.Application.Repositories;
using Marvel.Infrastruture.Context;
using System.Collections.Generic;

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

        public Task DeleteAsync<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            return _context.Set<T>().ToList();
        }

        public Task<T> GetByIdAsync<T>(Guid id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync<T>(int id) where T : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
