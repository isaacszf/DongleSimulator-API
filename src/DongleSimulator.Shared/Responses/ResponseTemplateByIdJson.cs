namespace Shared.Responses;

public class ResponseTemplateByIdJson : ResponseSourceJson
{
    public string Username { get; set; } = string.Empty;
    public int Replaces { get; set; }
    public DateTime CreatedAt { get; set; }
}