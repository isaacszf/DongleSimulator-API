using Microsoft.AspNetCore.Http;

namespace Shared.Requests;

public class RequestSendImageJson
{
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public IFormFile Image { get; set; } = null!;
}