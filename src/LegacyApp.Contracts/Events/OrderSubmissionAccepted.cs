namespace LegacyApp.Contracts.Events;

public record OrderSubmissionAccepted
{
    public string? OrderNumber { get; init; }
    public decimal? Amount { get; init; }
}