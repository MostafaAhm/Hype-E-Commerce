using Microsoft.EntityFrameworkCore;
using Product.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Persistence
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private DbSet<T> Dbset { get; set; }

        public BaseRepository(AppDbContext context)
        {
            _context = context;
            Dbset = context.Set<T>();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await Dbset.Where(expression).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await Dbset.ToListAsync();
        }
        public IQueryable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return Dbset.Where(predicate).AsNoTracking().AsQueryable();
        }
        public async Task<T> AddAsync(T entity)
        {
            Dbset.Add(entity);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            Dbset.AddRangeAsync(entities);
            return entities;
        }

        public async Task DeleteAsync(T entity)
        {
            Dbset.Remove(entity);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public void Add(T entity)
        {
            Dbset.Add(entity);
        }
    }
}
