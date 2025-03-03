using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SadidServices.Materials.Shared.Primitives;
using SadidServices.Materials.Shared.Primitives.Contracts;

namespace SadidServices.Materials.Infrastructure.Persistance.Repositories
{
    //! SQL Server - Oracle - MongoDB or ...
    //! Using SQL Repository
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TId : notnull
    {
        //! First Step For Connecting into SQL is Injection DbContext into our Class:
        private DbContext _context;
        private DbSet<TEntity> _set;
        //! inject Context and Set to Constructor:
        public Repository(DbContext context)
        {
            _context = context;
            _set = context.Set<TEntity>();
        }

        public async Task<TId> AddAsync(TEntity entity)
        {
            await _set.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //return await _set.Where(predicate).ToListAsync(); Better Performance use AsNoTracking :
            return await _set.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TId id)
        {
            return await _set.FindAsync(id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }
    }
}