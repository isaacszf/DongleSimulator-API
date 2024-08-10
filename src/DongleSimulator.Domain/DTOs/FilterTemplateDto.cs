using DongleSimulator.Domain.Enums;

namespace DongleSimulator.Domain.DTOs;

public record FilterTemplateDto
{
    public string? Title { get; init; }
    public string? Username { get; init; }
    public IList<Status> Status { get; init; } = [];
    public int? Replaces { get; init; }
    public bool? OrderByMostRecent { get; init; } = true;
}