using Demo.BLL.DTOS;
using Demo.BLL.Factories;
using Demo.DAL.Data.Contexts;
using Demo.DAL.Data.Repositories;
using Demo.DAL.Models;

namespace Demo.BLL.Services;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }
    //Get All ==> Id, Code, Name, Description, DateOfCreation [Date Part Only]



    public IEnumerable<DepartmentDto> GetAllDepartments()
    {
        var departments = _departmentRepository.GetAll();
        var departmentsToReturnDto = departments.Select(d => d.ToDepartmentDto());
        return departmentsToReturnDto;
    }
    
    //Get By Id

    public DepartmentDetailsDto GetById(int id)
    {
        var department = _departmentRepository.GetById(id);
        if(department == null)
            return null;

        // Mapping Types :
        // 1] Auto Mapping Package [AutoMapper]
        // 2] Manual Mapping 
        // 3] Extension Method Mapping 
        // 4] Constructor Mapping
        return department.ToDepartmentDetailsDto();
        // mapping ==> Department to DepartmentDetailsDto
    }

    // Add
    public int AddDpartment(CreateDepartmentDto departmentDto)
    {
        var department = departmentDto.ToEntity();
        return _departmentRepository.Add(department);
    }
    
    // Update

    public int UpdateDepartment(UpdateDepartmentDto departmentDto)
    {
        return _departmentRepository.Update(departmentDto.ToEntity());
    }
    
    // Remove

    public bool RemoveDepartment(int id)
    {
        var department = _departmentRepository.GetById(id);
        if(department == null)
            return false;
        return _departmentRepository.Remove(department) > 0;
    }
}

