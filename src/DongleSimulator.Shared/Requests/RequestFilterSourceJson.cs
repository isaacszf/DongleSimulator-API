using Shared.Responses.Enums;

namespace Shared.Requests;

public class RequestFilterSourceJson
{
    public string? Title { get; set; }
    public string? Username { get; set; }
    public IList<Status> Status { get; set; } = [];
    public bool? OrderByMostRecent{ get; set; } = true;
}