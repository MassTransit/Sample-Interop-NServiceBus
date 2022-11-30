namespace LegacyApp.Contracts.Commands;

public record SubmitOrder
{
    public string? OrderNumber { get; init; }
    public decimal? Amount { get; init; }
}