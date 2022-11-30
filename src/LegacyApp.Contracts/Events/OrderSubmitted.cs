namespace LegacyApp.Contracts.Events;

public record OrderSubmitted
{
    public string? OrderNumber { get; init; }
    public decimal? Amount { get; init; }
}