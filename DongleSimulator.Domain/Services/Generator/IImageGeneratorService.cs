namespace DongleSimulator.Domain.Services.Generator;

public interface IImageGeneratorService
{
    public Task<Stream> Generate(IList<string> sources, string templateUrl, int replaces);
}