using System.Linq.Expressions;
using Demo.DAL.Models.Shared;

namespace Demo.DAL.Data.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity 
{
    IEnumerable<TEntity> GetAll(bool withTracking = false);
    IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> selector);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
    TEntity? GetById(int id);
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    IEnumerable<TEntity> GetIEnumerable();
    IQueryable<TEntity> GetIQueryable();
}