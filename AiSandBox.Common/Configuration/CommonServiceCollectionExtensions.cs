using AiSandBox.Common.EventAggregator;
using Microsoft.Extensions.DependencyInjection;

namespace AiSandBox.Common.Extensions;

public static class CommonServiceCollectionExtensions
{
    public static IServiceCollection AddEventAggregator(this IServiceCollection services)
    {
        services.AddSingleton<IEventAggregator, EventAggregator.EventAggregator>();
        return services;
    }
}