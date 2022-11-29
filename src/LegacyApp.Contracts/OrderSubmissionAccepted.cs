namespace LegacyApp.Contracts;

using NServiceBus;


public record OrderSubmissionAccepted :
    IMessage
{
    public string? OrderNumber { get; init; }
    public decimal? Amount { get; init; }
}