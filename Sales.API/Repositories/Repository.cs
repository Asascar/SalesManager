using Microsoft.EntityFrameworkCore;
using Sales.API.Context;
using Sales.API.Repositories.Interfaces;

namespace Sales.API.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<T>> Get(int skip = 0, int take = 0)
        {
            if (take <= 0)
            {
                return await _context.Set<T>().AsNoTracking().ToListAsync();
            }

            return await _context.Set<T>()
                                 .AsNoTracking()
                                 .Skip(skip)
                                 .Take(take)
                                 .ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Add(T entity)
        {
            var newEntity = await _context.Set<T>().AddAsync(entity);
            _context.SaveChanges();

            return newEntity.Entity;
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
