namespace Shared.Responses;

public class ResponseSourceByIdJson : ResponseSourceJson
{
    public string Username { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}