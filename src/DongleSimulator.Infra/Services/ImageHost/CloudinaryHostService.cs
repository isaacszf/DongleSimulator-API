using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DongleSimulator.Domain.Services.ImageHost;

namespace DongleSimulator.Infra.Services.ImageHost;

public class CloudinaryHostService : IStorageImageService
{
    private readonly Cloudinary _cloudinaryClient;

    public CloudinaryHostService(Cloudinary cloudinaryClient)
    {
        _cloudinaryClient = cloudinaryClient;
    }
    
    public async Task Upload(Stream file, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, file),
            PublicId = fileName,
            UniqueFilename = false,
            Overwrite = true,
            Invalidate = true
        };

        await _cloudinaryClient.UploadAsync(uploadParams);
    }

    public async Task Delete(string imageIdentifier)
    {
        var deletionParams = new DeletionParams(imageIdentifier)
        {
            ResourceType = ResourceType.Image
        };

        await _cloudinaryClient.DestroyAsync(deletionParams);
    }
    
    public string GetUrlByImageIdentifier(string imageIdentifier)
    {
        var ext = imageIdentifier.Split('.')[1];
        
        return _cloudinaryClient.Api.UrlImgUp
            .Secure()
            .BuildUrl($"{imageIdentifier}.{ext}");
    }
}