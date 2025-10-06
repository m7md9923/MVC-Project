namespace Demo.BLL.DTOS;

public class DepartmentDto
{
    public int DeptID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateOnly DateOfCreation { get; set; }
}