namespace Shared.Responses;

public class ResponseTemplatesFromUserJson
{
    public IList<ResponseSourceJson> Templates { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}