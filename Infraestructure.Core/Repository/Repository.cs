using Infraestructure.Core.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Core.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly CoreContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(CoreContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties) {

            IQueryable<TEntity> query = AsQueryable();
            return await PerformInclusions(includeProperties, query);
        }
        public async Task<TEntity?> GetByIdAsync(int id) => await _entities.FindAsync(id);
        public async Task AddAsync(TEntity entity) => await _entities.AddAsync(entity);
        public void Update(TEntity entity) => _entities.Update(entity);
        public void Delete(TEntity entity) => _entities.Remove(entity);


        // Include lambda expressions in queries
        private Task<IQueryable<TEntity>> PerformInclusions(IEnumerable<Expression<Func<TEntity, object>>> includeProperties,
                                               IQueryable<TEntity> query)
        {
            var result = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return Task.FromResult(result);
        }
        public IQueryable<TEntity> AsQueryable()
        {
            return  _entities.AsQueryable<TEntity>();
        }
    }
}
