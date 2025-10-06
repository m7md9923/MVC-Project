using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories.Interfaces;
using Microsoft.Identity.Client;

namespace Demo.DAL.Data.Repositories.Classes;

public class UnitOfWork : IUnitOfWork, IDisposable
{

    private readonly Lazy<IEmployeeRepository> _employeeRepository;
    private readonly Lazy<IDepartmentRepository> _departmentRepository;
    private readonly ApplicationDbContext _dbContext;
    
    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _employeeRepository = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(dbContext));
        _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dbContext));
    }
    public IEmployeeRepository EmployeeRepository { get => _employeeRepository.Value; }
    public IDepartmentRepository DepartmentRepository { get => _departmentRepository.Value; }

    public int SaveChanges()  // Num of Rows Affected
    {
        return _dbContext.SaveChanges();
    }
    
    public void Dispose() 
    {
        _dbContext.Dispose();
    }
    
}