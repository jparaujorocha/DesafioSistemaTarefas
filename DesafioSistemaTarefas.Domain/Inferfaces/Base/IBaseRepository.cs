using System.Linq.Expressions;

namespace DesafioSistemaTarefas.Domain.Inferfaces.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetById(int id);
        Task<TEntity> Add(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
        Task<int> Commit();
    }
}
