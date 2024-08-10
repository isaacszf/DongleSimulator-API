using Shared.Responses.Enums;

namespace Shared.Responses;

public class ResponseUserJson
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public Guid UserIdentifier { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Role { get; set; } = UserRole.Default;
}