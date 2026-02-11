using AiSandBox.Ai.Configuration;
using AiSandBox.ApplicationServices.Configuration;
using AiSandBox.ApplicationServices.Runner;
using AiSandBox.Common.Extensions;
using AiSandBox.ConsolePresentation.Configuration;
using AiSandBox.Domain.Configuration;
using AiSandBox.Infrastructure.Configuration;
using AiSandBox.SharedBaseTypes.ValueObjects;
using AiSandBox.Startup.Configuration;
using AiSandBox.WebApi.Configuration;
using ConsolePresentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

StartupSettings startupSettings = builder.Configuration.GetSection("StartupSettings").Get<StartupSettings>() ?? new StartupSettings();

bool isWebApiEnabled = startupSettings.IsWebApiEnabled;

builder.Services.AddEventAggregator();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddAiSandBoxServices(startupSettings.PresentationType);
if (startupSettings.PresentationType == PresentationType.Console)
{
    builder.Services.AddConsolePresentationServices(builder.Configuration, builder.Configuration);
    builder.Configuration.AddJsonFile("ConsoleSettings.json", optional: false, reloadOnChange: true);
}

if (isWebApiEnabled)
    builder.Services.AddWebApiPresentationServices();

var app = builder.Build();

if (startupSettings.PresentationType == PresentationType.Console)
{
    app.Services.GetRequiredService<IConsoleRunner>().Run();

    if (!startupSettings.TestPreconditionsEnabled)
        await app.Services.GetRequiredService<IExecutorForPresentation>().RunAsync();
    else
        await app.Services.GetRequiredService<IExecutorForPresentation>().TestRunWithPreconditionsAsync();
}
    
if (isWebApiEnabled)
{
    app.MapControllers();
    await app.RunAsync();
}