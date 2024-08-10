namespace Shared.Responses;

public class ResponseSourcesFromUserJson
{
    public IList<ResponseSourceJson> Sources { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}