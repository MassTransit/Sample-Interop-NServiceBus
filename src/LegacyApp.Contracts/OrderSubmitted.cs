namespace LegacyApp.Contracts;

using NServiceBus;


public record OrderSubmitted :
    IEvent
{
    public string? OrderNumber { get; init; }
    public decimal? Amount { get; init; }
}