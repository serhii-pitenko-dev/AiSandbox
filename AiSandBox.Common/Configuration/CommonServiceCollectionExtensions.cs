using AiSandBox.Common.MessageBroker;
using Microsoft.Extensions.DependencyInjection;

namespace AiSandBox.Common.Extensions;

public static class CommonServiceCollectionExtensions
{
    public static IServiceCollection AddEventAggregator(this IServiceCollection services)
    {
        services.AddSingleton<IMessageBroker, MessageBroker.MessageBroker>();
        return services;
    }
}