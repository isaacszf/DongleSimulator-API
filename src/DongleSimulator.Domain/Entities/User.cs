using DongleSimulator.Domain.Enums;

namespace DongleSimulator.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = UserRole.Default;
    public Guid UserIdentifier { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public IList<Template> Templates { get; set; }  = [];
    public IList<Source> Sources { get; set; } = [];
}