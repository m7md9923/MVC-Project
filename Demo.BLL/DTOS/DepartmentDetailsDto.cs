namespace Demo.BLL.DTOS;

public class DepartmentDetailsDto
{
    public int Id { get; set; }
    public DateOnly? CreatedOn { get; set; } // The Date Time of creating the record 
    public DateOnly? ModifiedOn { get; set; } // The Date Time of modifying the record 
    public bool IsDeleted { get; set; }  // Soft Delete
    public int CreatedBy { get; set; }  // User Id
    public int ModifiedBy { get; set; }  // User Id
    
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; } 
}