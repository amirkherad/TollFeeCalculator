using TollFeeCalculator.Infrastructure.EventMessages.Contracts;

namespace TollFeeCalculator.Infrastructure.EventMessages;

/// <summary>
/// Sending messages to bus
/// </summary>
public class Bus 
    : IBus
{
    /// <summary>
    /// Send a message to bus
    /// </summary>
    /// <param name="message"></param>
    public void Send(string message)
    {
        // Put the message on the bus
        Console.WriteLine($"LogMessage sent: '{message}'");
    }
}