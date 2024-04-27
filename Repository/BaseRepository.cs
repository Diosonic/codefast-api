using Codefast.Context;
using Codefast.Repository.Interfaces;

namespace Codefast.Repository
{
    public class BaseRepository : IBaseRepository
    {
        private readonly CodefastContext _context;

        public BaseRepository(CodefastContext context)
        {
            _context = context;
        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            _context.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
