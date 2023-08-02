using Microsoft.Extensions.DependencyInjection;
using TollFeeCalculator.Infrastructure.EventMessages;
using TollFeeCalculator.Infrastructure.EventMessages.Contracts;

namespace TollFeeCalculator.Infrastructure.Shared.Extensions.DependencyInjection;

public static class DomainEventsExtensions
{
    public static IServiceCollection AddDomainEventsDispatcher(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>()
            .AddScoped<IBus, Bus>();

        return serviceCollection;
    }
}