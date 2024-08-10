namespace Shared.Responses;

public class ResponsesUsersJson
{
    public IList<ResponseUserJson> Users { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
}