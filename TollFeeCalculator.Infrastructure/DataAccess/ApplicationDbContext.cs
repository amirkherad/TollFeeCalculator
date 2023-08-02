using Microsoft.EntityFrameworkCore;
using TollFeeCalculator.Domain.Contracts;
using TollFeeCalculator.Domain.Shared.BaseClasses;
using TollFeeCalculator.Infrastructure.EventMessages;
using TollFeeCalculator.Infrastructure.EventMessages.Contracts;
using TollFeeCalculator.Infrastructure.Shared.Extensions;

namespace TollFeeCalculator.Infrastructure.DataAccess;

/// <summary>
/// The main implemented class of DbContext
/// </summary>
public class ApplicationDbContext
    : DbContext
{
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    /// <summary>
    /// The main constructor
    /// </summary>
    /// <param name="options"></param>
    /// <param name="domainEventsDispatcher"></param>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IDomainEventsDispatcher domainEventsDispatcher) : base(options)
    {
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    /// <summary>
    /// An overriding of OnModelCreating to apply EntityTypeConfigurations using fluent api
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var entityAssemblies = typeof(IEntity).Assembly;
        modelBuilder.RegisterAllEntities<IEntity>(entityAssemblies);
        
        var configurationAssemblies = typeof(IInfrastructureMarker).Assembly;
        modelBuilder.ApplyEntityTypeConfigurations(configurationAssemblies);
    }

    /// <summary>
    /// An overriding of SaveChangesAsync to dispatch events from change tracker
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var aggregateRoots = ChangeTracker
            .Entries()
            .Where(x => x.Entity is BaseEntity)
            .Select(x => (BaseEntity)x.Entity)
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        if (result == 0)
            return result;

        foreach (var aggregateRoot in aggregateRoots)
        {
            _domainEventsDispatcher.Dispatch(aggregateRoot.DomainEvents);
            aggregateRoot.ClearDomainEvents();
        }

        return result;
    }
}