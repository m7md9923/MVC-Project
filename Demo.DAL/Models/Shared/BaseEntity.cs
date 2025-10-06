namespace Demo.DAL.Models.Shared;

public class BaseEntity // Include Common Props [Parent]
{
    public int Id { get; set; }
    public DateTime? CreatedOn { get; set; } // The Date Time of creating the record 
    public DateTime? ModifiedOn { get; set; } // The Date Time of modifying the record 
    public bool IsDeleted { get; set; }  // Soft Delete
    public int CreatedBy { get; set; }  // User Id
    public int ModifiedBy { get; set; }  // User Id
}