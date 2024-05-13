using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DataAccess.Repositories;

public class CloudinaryRepository: ICloudinaryRepository
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryRepository(Cloudinary cloudinary) 
    {
        _cloudinary = cloudinary;
    }
    
    public ImageUploadResult UploadImage(IFormFile file, string publicId)
    {
        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = publicId,
        };

        var uploadResult =  _cloudinary.Upload(uploadParams);
            
        return uploadResult;
    }

    public string GetImageUrl(string publicId)
    {
        var getResourceResult = _cloudinary.GetResource(publicId);
        var url = getResourceResult.SecureUrl;
        return url;
    }

    
    public ImageUploadResult UpdateImage(IFormFile file, string publicId)
    {
        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = publicId,
            Overwrite = true
        };

        var uploadResult =  _cloudinary.Upload(uploadParams);
            
        return uploadResult;
    }       
    
    public DeletionResult DeleteImage(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        return _cloudinary.Destroy(deleteParams);
    }    
}