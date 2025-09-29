using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories.Interfaces;
using Demo.DAL.Models.Shared;

namespace Demo.DAL.Data.Repositories.Classes;

public class GenericRepository<TEntity> (ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    public IEnumerable<TEntity> GetAll(bool withTracking = false)
    {
        if (withTracking)
        {
            return _dbContext.Set<TEntity>().Where(e => !e.IsDeleted).ToList();
        }
        else
        {
            return _dbContext.Set<TEntity>().Where(e => !e.IsDeleted).AsNoTracking().ToList();
        }
    }
    // 2] Get By Id
    
    public TEntity? GetById(int id)
    {
        return _dbContext.Set<TEntity>().Find(id);
    }
    
    // 3] ADD

    public int Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        // dbContext.Add(department);
        // dbContext.Set<Department>().Add(department);
        
        return _dbContext.SaveChanges();
    }
    // 4] Update

    public int Update(TEntity entity)
    {
        _dbContext.Set<TEntity>().Update(entity);
        return _dbContext.SaveChanges();
    }
    
    // 5] Remove
    
    public int Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        return _dbContext.SaveChanges();
    }
}