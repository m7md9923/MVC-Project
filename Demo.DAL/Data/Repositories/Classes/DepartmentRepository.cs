using Demo.DAL.Data.Contexts;

namespace Demo.DAL.Data.Repositories;

// PL ==> Controller ==> Services ==> Repository ==> DbContext ==> DB  

public class DepartmentRepository : IDepartmentRepository
{
    
    public ApplicationDbContext dbContext { get; }
    // Ask CLR to create the Object from ApplicationDbContext 
    public DepartmentRepository(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    
    // Inject Obj needed
    // LifeTime [memory]
    
 
    // 5 CRUD Operations:  
    // 1] Get All
    
    public IEnumerable<Department> GetAll(bool withTracking = false)
    {
        if (withTracking)
        {
            return dbContext.Departments.ToList();
        }
        else
        {
            return dbContext.Departments.AsNoTracking().ToList();
        }
    }
    // 2] Get By Id
    
    public Department? GetById(int id)
    {
        return dbContext.Departments.Find(id);
    }
    
    // 3] ADD

    public int Add(Department department)
    {
        dbContext.Departments.Add(department);
        // dbContext.Add(department);
        // dbContext.Set<Department>().Add(department);
        
        return dbContext.SaveChanges();
    }
    // 4] Update

    public int Update(Department department)
    {
        dbContext.Departments.Update(department);
        return dbContext.SaveChanges();
    }
    
    // 5] Remove
    
    public int Remove(Department department)
    {
        dbContext.Departments.Remove(department);
        return dbContext.SaveChanges();
    }


}

internal class DepartmentMockRepository
{
    
}