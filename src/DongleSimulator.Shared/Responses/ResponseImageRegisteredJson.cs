namespace Shared.Responses;

public class ResponseImageRegisteredJson
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageIdentifier { get; set; } = string.Empty;
}