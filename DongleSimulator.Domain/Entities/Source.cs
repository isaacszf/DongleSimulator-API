using DongleSimulator.Domain.Enums;

namespace DongleSimulator.Domain.Entities;

public class Source
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Subtitle { get; set; } = string.Empty;
    public string ImageIdentifier { get; set; } = string.Empty;
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public long UserId { get; set; }
    public User User { get; set; } = null!;
}