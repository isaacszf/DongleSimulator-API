using FileTypeChecker.Extensions;
using FileTypeChecker.Types;

namespace DongleSimulator.Application.Extensions;

public static class StreamImageExtensions
{
    public static (bool isImageValid, string extension) ValidateAndGetImageExtension(this Stream stream)
    {
        var result = (false, string.Empty);

        if (stream.Is<PortableNetworkGraphic>())
            result = (true, NormalizeExtension(PortableNetworkGraphic.TypeExtension));
        else if (stream.Is<JointPhotographicExpertsGroup>())
            result = (true, NormalizeExtension(JointPhotographicExpertsGroup.TypeExtension));

        stream.Position = 0;

        return result;
    }

    private static string NormalizeExtension(string extension) 
        => extension.StartsWith('.') ? extension : $".{extension}";
}