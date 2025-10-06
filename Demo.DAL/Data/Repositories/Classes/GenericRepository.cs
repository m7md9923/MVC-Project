using System.Linq.Expressions;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories.Interfaces;
using Demo.DAL.Models.Shared;

namespace Demo.DAL.Data.Repositories.Classes;

public class GenericRepository<TEntity> (ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    public IEnumerable<TEntity> GetAll(bool withTracking = true)
    {
        if (withTracking)
        {
            return _dbContext.Set<TEntity>().Where(e => !e.IsDeleted).ToList();
        }
        else
        {
            return _dbContext.Set<TEntity>().Where(e => !e.IsDeleted).AsTracking().ToList();
        }
    }

    public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector)
    {
        return _dbContext.Set<TEntity>().Where(e => !e.IsDeleted)
            .Select(selector).ToList(); // Deffered ==> Immediate
    }

    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
    {
        return _dbContext.Set<TEntity>().Where(predicate).ToList();
    }

    // 2] Get By Id
    
    public TEntity? GetById(int id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }
    
    // 3] ADD

    public void Add(TEntity entity) 
    {
        _dbContext.Set<TEntity>().Add(entity); // Add locally
        // dbContext.Add(department);
        // dbContext.Set<Department>().Add(department);

    }
    // 4] Update

    public void Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
    }
    
    // 5] Remove
    
    public void Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
    }

    public IEnumerable<TEntity> GetIEnumerable()
    {
        return _dbContext.Set<TEntity>();
    }

    public IQueryable<TEntity> GetIQueryable()
    {
        return _dbContext.Set<TEntity>();
    }
}