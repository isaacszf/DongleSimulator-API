namespace DongleSimulator.Domain.Services.ImageHost;

public interface IStorageImageService
{
    public Task Upload(Stream file, string fileName);
    public Task Delete(string imageIdentifier);
    public string GetUrlByImageIdentifier(string imageIdentifier);
}