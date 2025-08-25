using System.Linq.Expressions;

namespace Infraestructure.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> Find(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>?> FindAll(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity?> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
