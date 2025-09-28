using Demo.BLL.DTOS.EmployeeDTOS;
using Demo.DAL.Models.EmployeeModule;

namespace Demo.BLL.Services.Interfaces;

public interface IEmployeeService
{
    // 1] GEtAll
    IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false);
    
    // 2] GetBy Id
    EmployeeDetailsDto GetById(int id);
    
    // 3] Create Employee
    int CreateEmployee(CreateEmployeeDto employeeDto);
    
    // 4] Update Employee
    int UpdateEmployee(UpdateEmployeeDto employeeDto);
    
    // 5] Delete Employee
    bool RemoveEmployee(int id);
}