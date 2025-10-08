using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services.AttachmentService;

public interface IAttachmentService
{
    // Upload 
    public string? Upload(IFormFile file, string folderName);
    
    // Delete
    public bool Delete(string filePath);
}