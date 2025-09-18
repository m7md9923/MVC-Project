using Demo.BLL.DTOS;

namespace Demo.BLL.Services;

public interface IDepartmentService
{
    IEnumerable<DepartmentDto> GetAllDepartments();
    DepartmentDetailsDto GetById(int id);
    int AddDpartment(CreateDepartmentDto departmentDto);
    int UpdateDepartment(UpdateDepartmentDto departmentDto);
    bool RemoveDepartment(int id);
}