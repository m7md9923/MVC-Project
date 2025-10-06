using Demo.BLL.DTOS;
using Demo.DAL.Models;
using Demo.DAL.Models.DepartmentModule;

namespace Demo.BLL.Factories;

public static class DepartmentFactory
{
    public static DepartmentDto ToDepartmentDto(this Department d)
    {
        return new DepartmentDto()
        {
            DeptID = d.Id,
            Name = d.Name,
            Code = d.Code,
            Description = d.Description,
            DateOfCreation = d.CreatedOn.HasValue ? DateOnly.FromDateTime(d.CreatedOn.Value) : default
        };
    }

    public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
    {

        return new DepartmentDetailsDto()
        {
            Id = department.Id,
            Code = department.Code,
            Description = department.Description,
            Name = department.Name,
            CreatedBy = department.CreatedBy,
            ModifiedBy = department.ModifiedBy,
            CreatedOn = department.CreatedOn.HasValue ? DateOnly.FromDateTime(department.CreatedOn.Value) : default,
            ModifiedOn = department.ModifiedOn.HasValue ? DateOnly.FromDateTime(department.ModifiedOn.Value) : default,
            IsDeleted = department.IsDeleted
        };
    }

    public static Department ToEntity(this CreateDepartmentDto department)
    {
        return new Department()
        {
            Description = department.Description,
            Code = department.Code,
            Name = department.Name,
            CreatedOn = department.DateOfCreation.ToDateTime(new TimeOnly())
        };
    }
    
    public static Department ToEntity(this UpdateDepartmentDto department)
    {
        return new Department()
        {
            Id = department.Id,
            Description = department.Description,
            Code = department.Code,
            Name = department.Name,
            CreatedOn = department.DateOfCreation.ToDateTime(new TimeOnly())
        };
    }
}