using Shared.Responses.Enums;

namespace Shared.Requests;

public class RequestFilterTemplateJson
{
    public string? Title { get; init; }
    public string? Username { get; init; }
    public IList<Status> Status { get; init; } = [];
    public int? Replaces { get; init; }
    public bool? OrderByMostRecent { get; init; } = true;
}