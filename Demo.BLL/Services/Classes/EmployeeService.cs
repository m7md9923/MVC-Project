using AutoMapper;
using Demo.BLL.DTOS.EmployeeDTOS;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Data.Repositories.Interfaces;
using Demo.DAL.Models.EmployeeModule;

namespace Demo.BLL.Services.Classes;

public class EmployeeService(IEmployeeRepository _employeeRepository , IMapper _mapper) : IEmployeeService
{
    public IEnumerable<EmployeeDto> GetAllEmployees(bool withTracking = false)
    {
        var employees = _employeeRepository.GetAll(withTracking);
        // var employeesDto =  employees.Select(e => new EmployeeDto
        // {
        //     Id = e.Id,
        //     Name = e.Name,
        //     Age = e.Age,
        //     Salary = e.Salary,
        //     IsActive = e.IsActive,
        //     Gender = e.Gender.ToString(),  // Enum ==> String
        //     EmployeeType = e.EmployeeType.ToString() 
        // });
        var employeesDto = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeDto> >(employees);
        return employeesDto;
    }

    public EmployeeDetailsDto GetById(int id)
    {
        var employee = _employeeRepository.GetById(id);
        if(employee is null)
            return null;
        // return new EmployeeDetailsDto()
        // {
        //     Id = employee.Id,
        //     Name = employee.Name,
        //     Age = employee.Age,
        //     Salary = employee.Salary,
        //     Address = employee.Address,
        //     Email = employee.Email,
        //     PhoneNumber = employee.PhoneNumber,
        //     HiringDate = DateOnly.FromDateTime(employee.HiringDate),
        //     IsActive = employee.IsActive,
        //     Gender = employee.Gender.ToString(), // Enum ==> String
        //     EmployeeType = employee.EmployeeType.ToString(),
        //     ModifiedBy = 1,
        //     CreatedBy = 1,
        //     ModifiedOn = employee.ModifiedOn,
        //     CreatedOn = employee.CreatedOn
        // };
        var employeeDto = _mapper.Map<Employee, EmployeeDetailsDto>(employee);
        return employeeDto;
    }

    public int CreateEmployee(CreateEmployeeDto employeeDto)
    {
        var employee = _mapper.Map<CreateEmployeeDto, Employee>(employeeDto);
       return _employeeRepository.Add(employee);
    }

    public int UpdateEmployee(UpdateEmployeeDto employeeDto)
    {
        var employee = _mapper.Map<UpdateEmployeeDto, Employee>(employeeDto);
        return _employeeRepository.Update(employee);
    }

    public bool RemoveEmployee(int id)
    {
        // Soft Delete ==> like do update but [IsDeleted = true]
        var employee = _employeeRepository.GetById(id);
        if(employee == null)
            return false;
        employee.IsDeleted = true;
        return _employeeRepository.Update(employee) > 0;
    }
}