using TollFeeCalculator.Application.Shared.Contracts;

namespace TollFeeCalculator.Application.Shared.BaseClasses;

/// <summary>
/// The base command model
/// </summary>
public class BaseCommand
    : ICommand
{
    /// <summary>
    /// The identifier of command model
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The default constructor
    /// </summary>
    protected BaseCommand()
    {
    }

    /// <summary>
    /// The constructor to set identifier
    /// </summary>
    /// <param name="id"></param>
    protected BaseCommand(int id)
    {
        Id = id;
    }
}

/// <summary>
/// The base generic command model
/// </summary>
/// <typeparam name="TResult"></typeparam>
public class BaseCommand<TResult>
    : ICommand<TResult>
{
    /// <summary>
    /// The identifier of command model
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The default constructor
    /// </summary>
    protected BaseCommand()
    {
    }

    /// <summary>
    /// The constructor to set identifier
    /// </summary>
    /// <param name="id"></param>
    protected BaseCommand(int id)
    {
        Id = id;
    }
}