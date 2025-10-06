using Demo.DAL.Models.Shared;

namespace Demo.DAL.Data.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity 
{
    IEnumerable<TEntity> GetAll(bool withTracking = false);
    TEntity? GetById(int id);
    int Add(TEntity entity);
    int Update(TEntity entity);
    int Remove(TEntity entity);
}