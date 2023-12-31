using Microsoft.AspNetCore.Http;

namespace ShareResource.DTO.File;

public class FileUploadDto
{
    public IFormFile FileDetails { get; set; }
    public FileType FileType { get; set; }
}