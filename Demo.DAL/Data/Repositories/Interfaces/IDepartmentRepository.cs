using Demo.DAL.Data.Contexts;

namespace Demo.DAL.Data.Repositories;

public interface IDepartmentRepository
{
    ApplicationDbContext dbContext { get; }
    IEnumerable<Department> GetAll(bool withTracking = false);
    Department? GetById(int id);
    int Add(Department department);
    int Update(Department department);
    int Remove(Department department);
}