using Demo.DAL.Models.Shared;

namespace Demo.DAL.Models.EmployeeModule;

public class Employee : BaseEntity
{
    public string Name { get; set; } = null!;
    public int Age { get; set; }
    public string? Address { get; set; }
    public decimal Salary { get; set; }
    public bool IsActive { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime HiringDate { get; set; }
    
    // Gender ==> [Male-Female]
    // EmployeeType ==> [PartTime-FullTime]
    public Gender Gender { get; set; }
    public EmployeeType EmployeeType { get; set; }
    
}