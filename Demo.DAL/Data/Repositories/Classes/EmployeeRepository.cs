using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories.Interfaces;
using Demo.DAL.Models.DepartmentModule;
using Demo.DAL.Models.EmployeeModule;

namespace Demo.DAL.Data.Repositories.Classes;

public class EmployeeRepository(ApplicationDbContext _dbContext) : GenericRepository<Employee>(_dbContext) ,IEmployeeRepository
{
   
}