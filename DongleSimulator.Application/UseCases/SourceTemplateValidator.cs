namespace DongleSimulator.Application.UseCases;

public static class SourceTemplateValidator
{
    public static bool IsTitleInvalid(string title) => title.Length is > 20 or < 3;
    
    public static bool IsSubtitleInvalid(string subtitle) => subtitle.Length is > 40 or < 6;
}