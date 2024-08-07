using Shared.Responses.Enums;

namespace Shared.Responses;

public class ResponseSourceJson
{
    public string Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public Status Status { get; set; }
}