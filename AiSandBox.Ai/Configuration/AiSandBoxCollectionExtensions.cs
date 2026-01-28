using AiSandBox.Ai.AgentActions;
using AiSandBox.SharedBaseTypes.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace AiSandBox.Ai.Configuration;

public static class AiSandBoxCollectionExtensions
{
    public static IServiceCollection AddAiSandBoxServices(this IServiceCollection services, PresentationType presentationType)
    {
        if (presentationType is PresentationType.None)
        {
            services.AddTransient<IAiActions, RandomActions>();
        }
        else
        {
            services.AddTransient<IAiActions, RandomActions>();
        }
        
        return services;
    }
}

