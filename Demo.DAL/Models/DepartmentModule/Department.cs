using Demo.DAL.Models.Shared;

namespace Demo.DAL.Models.DepartmentModule;

public class Department : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; } 
}