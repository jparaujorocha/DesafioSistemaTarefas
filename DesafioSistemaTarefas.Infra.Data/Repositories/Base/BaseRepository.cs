using DesafioSistemaTarefas.Domain.Exceptions;
using DesafioSistemaTarefas.Domain.Inferfaces.Base;
using DesafioSistemaTarefas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace DesafioSistemaTarefas.Infra.Data.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        protected readonly ILogger _logger;

        protected BaseRepository(ApplicationDbContext context, ILogger<BaseRepository<TEntity>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task<TEntity> Add(TEntity entity)
        {
            _logger.LogInformation("Start Operation to add entity: " + typeof(TEntity).FullName);
            try
            {
                var result = await _context.Set<TEntity>().AddAsync(entity);
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on insert entity: ", innerException: ex);
            }
        }
        public virtual async Task Delete(TEntity entity)
        {
            _logger.LogInformation("Start Operation to delete entity: " + typeof(TEntity).FullName);
            try
            {
                _context.Set<TEntity>().Remove(entity);

                await _context.SaveChangesAsync();

                _logger.LogInformation("Finish Operation to delete entity: " + typeof(TEntity).FullName);
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on delete entity: ", innerException: ex);
            }
        }
        public virtual async Task Update(TEntity entity)
        {
            _logger.LogInformation("Start Operation to update entity: " + typeof(TEntity).FullName);
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on update entity: ", innerException: ex);
            }
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            _logger.LogInformation("Start Operation to get all entities: " + typeof(TEntity).FullName);
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on get entities: ", innerException: ex);
            }
        }
        public virtual async Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            _logger.LogInformation("Start Operation to get all entities by predicaties: " + typeof(TEntity).FullName);
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().Where(predicate).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on get entities by predicaties: ", innerException: ex);
            }
        }
        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            _logger.LogInformation("Start Operation to get entity by predicate: " + typeof(TEntity).FullName);
            try
            {
                return await _context.Set<TEntity>().AsNoTracking().Where(predicate).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on get entity by predicate: ", innerException: ex);
            }
        }
        public virtual async Task<TEntity> GetById(int id)
        {
            _logger.LogInformation("Start Operation to get entity by id: " + typeof(TEntity).FullName);
            try
            {
                return await _context.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on get entitiy by id: ", innerException: ex);
            }
        }
        public async Task<int> Commit()
        {
            _logger.LogInformation("Start Operation to commit changes: " + typeof(TEntity).FullName);
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new DataBaseException("Error on commit changes on database: ", innerException: ex);
            }
        }

        public void Dispose()
        {
            _logger.LogInformation("Start Operation to dispose: " + typeof(TEntity).FullName);
            Dispose(true);
            GC.SuppressFinalize(this);
            _logger.LogInformation("Finish Operation to dispose: " + typeof(TEntity).FullName);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
