using CloudinaryDotNet.Actions;
using Core.Models;
using Microsoft.AspNetCore.Http;

namespace Core.Interfaces;

public interface ICloudinaryRepository
{
    ImageUploadResult UploadImage(IFormFile file, string publicId);
    
    string GetImageUrl(string publicId);

    ImageUploadResult UpdateImage(IFormFile file, string publicId);

    DeletionResult DeleteImage(string publicId);

}