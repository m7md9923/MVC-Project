namespace Demo.DAL.Data.Repositories.Interfaces;

public interface IUnitOfWork
{
    public IEmployeeRepository EmployeeRepository { get; }
    public IDepartmentRepository DepartmentRepository { get; }
    // Save Changes
    public int SaveChanges(); // num os rows it saved
}