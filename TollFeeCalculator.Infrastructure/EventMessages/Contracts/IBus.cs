namespace TollFeeCalculator.Infrastructure.EventMessages.Contracts;

/// <summary>
/// Sending messages to bus
/// </summary>
public interface IBus
{
    /// <summary>
    /// Send message to bus
    /// </summary>
    /// <param name="message"></param>
    void Send(string message);
}