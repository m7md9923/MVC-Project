using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Services.AttachmentService;

public class AttachmentService : IAttachmentService
{
    
    List<string> allowedExtensions = [ ".jpg", ".png", ".jpeg"];
    // 2MB ==> Bytes => (2*1024*1024)   
    const int maxFileSize = 2*1024*1024;
    
    public string? Upload(IFormFile file, string folderName)
    { 
        var extension = Path.GetExtension(file.FileName);
        if (!allowedExtensions.Contains(extension))
        {
            return null;
        }

        // and can be validating on min Size
        if (file.Length > maxFileSize || file.Length == 0)
        {
            return null;
        }
        // var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{folderName}";
        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images");

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(folderPath, fileName);
        
        // The using statement only works with objects that implement the interface IDisposable (or IAsyncDisposable for async cleanup).
        
        /*
            FileStream is a class in C# (System.IO) that lets you read from or write to files, byte by byte.

            Think of it like a pipe that connects your program to a file on the disk.
            * File streaming:
            1]You Open the Image File
            FileStream establishes a connection between your program and the image file.
            The file is opened in read, write, or both modes.

            2]You Read or Write Binary Data
            Images are stored as bytes (0s and 1s), not as human-readable text.

            3]You Close the File After Use
            Keeping an image file open too long can cause file locks or memory issues.
            Use using to ensure proper closure.
            */
        /*
         FileMode.CreateNew ==> Creates a new file.If the file already exists, throws an IOException
         FileMode.Create ==> Creates a new file. Overwrites if the file already exists.
         FileMode.Open ==> Opens an existing file. Throws an exception if it doesn't exist.
         FileMode.Append ==> Opens the file if it exists or creates a new one if not exist. Data is written to the end of the file.
         FileMode.Truncate ==> Opens an existing file and clears its content (length = 0).
         FileMode.OpenOrCreate ==> Opens the file if it exists or creates a new one if not exist.
         */
        using var stream = new FileStream(filePath, FileMode.Create);
        file.CopyTo(stream);
        return fileName;
    }

    public bool Delete(string filePath)
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return true;
        }
        return false;
    }
}