using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories.Classes;
using Demo.DAL.Data.Repositories.Interfaces;
using Demo.DAL.Models.DepartmentModule;
using Demo.DAL.Models.EmployeeModule;

namespace Demo.DAL.Data.Repositories;

// PL ==> Controller ==> Services ==> Repository ==> DbContext ==> DB  

public class DepartmentRepository(ApplicationDbContext _dbContext) : GenericRepository<Department>(_dbContext), IDepartmentRepository
{ 
}