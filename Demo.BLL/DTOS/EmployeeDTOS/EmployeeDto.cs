using System.ComponentModel.DataAnnotations;
using Demo.BLL.DTOS.DepartmentDTOS;
using Demo.DAL.Models.DepartmentModule;

namespace Demo.BLL.DTOS.EmployeeDTOS;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? Age { get; set; }
    [DataType(DataType.Currency)]
    public decimal Salary { get; set; }
    [Display(Name = "Is Active")]
    public bool IsActive { get; set; }
    [EmailAddress]
    public string? Email { get; set; }
    public string Gender { get; set; }
    [Display(Name = "Employee Type")]
    public string EmployeeType { get; set; }
    
    public string? Department { get; set; } // Dept Name
    
    [Display(Name = "Image")]
    public string? ImageName { get; set; }  
}