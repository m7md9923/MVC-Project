using Demo.BLL.DTOS;
using Demo.BLL.DTOS.DepartmentDTOS;
using Demo.BLL.Factories;
using Demo.BLL.Services.Interfaces;
using Demo.DAL.Data.Repositories;
using Demo.DAL.Data.Repositories.Interfaces;

namespace Demo.BLL.Services.Classes;

public class DepartmentService(IUnitOfWork _unitOfWork) : IDepartmentService
{
    //Get All ==> Id, Code, Name, Description, DateOfCreation [Date Part Only]
    

    public IEnumerable<DepartmentDto> GetAllDepartments()
    {
        var departments = _unitOfWork.DepartmentRepository.GetAll();
        var departmentsToReturnDto = departments.Select(d => d.ToDepartmentDto());
        return departmentsToReturnDto;
    }
    
    //Get By Id

    public DepartmentDetailsDto GetById(int id)
    {
        var department = _unitOfWork.DepartmentRepository.GetById(id);
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
        _unitOfWork.DepartmentRepository.Add(department);
        return _unitOfWork.SaveChanges();
    }
    
    // Update

    public int UpdateDepartment(UpdateDepartmentDto departmentDto)
    {
        _unitOfWork.DepartmentRepository.Update(departmentDto.ToEntity());
        return _unitOfWork.SaveChanges();
    }
    
    // Remove

    public bool RemoveDepartment(int id)
    {
        var department = _unitOfWork.DepartmentRepository.GetById(id);
        if(department == null)
            return false;
        _unitOfWork.DepartmentRepository.Remove(department);
        return _unitOfWork.SaveChanges() > 0;
    }
}

