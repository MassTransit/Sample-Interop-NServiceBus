namespace LegacyApp.Contracts;

using NServiceBus;


public record SubmitOrder :
    IMessage
{
    public string? OrderNumber { get; init; }
    public decimal? Amount { get; init; }
}