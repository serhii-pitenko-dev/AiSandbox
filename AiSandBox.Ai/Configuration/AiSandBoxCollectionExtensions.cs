using AiSandBox.Ai.AgentActions;
using AiSandBox.SharedBaseTypes.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace AiSandBox.Ai.Configuration;

public static class AiSandBoxCollectionExtensions
{
    public static IServiceCollection AddAiSandBoxServices(this IServiceCollection services, EPresentationType presentationType)
    {
        if (presentationType is EPresentationType.None)
        {
            services.AddTransient<IAiActions, RandomActionsForSimulation>();
        }
        else
        {
            services.AddTransient<IAiActions, RandomActionsForPresentation>();
        }
        
        return services;
    }
}

