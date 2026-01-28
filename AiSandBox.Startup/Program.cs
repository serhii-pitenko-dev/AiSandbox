using AiSandBox.Ai.Configuration;
using AiSandBox.ApplicationServices.Configuration;
using AiSandBox.ConsolePresentation.Configuration;
using AiSandBox.Domain.Configuration;
using AiSandBox.Infrastructure.Configuration;
using AiSandBox.SharedBaseTypes.ValueObjects;
using AiSandBox.WebApi.Configuration;
using ConsolePresentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AiSandBox.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

var presentationType =
    Enum.Parse<PresentationType>(
        builder.Configuration["PresentationType"]!,
        ignoreCase: true);

bool isWebApiEnabled = builder.Configuration.GetValue<bool>("IsWebApiEnabled");

builder.Services.AddEventAggregator();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();
builder.Services.AddControllers();
builder.Services.AddAiSandBoxServices(presentationType);
if (presentationType == PresentationType.Console)
{
    builder.Services.AddConsolePresentationServices(builder.Configuration, builder.Configuration);
    builder.Configuration.AddJsonFile("ConsoleSettings.json", optional: false, reloadOnChange: true);
}

if (isWebApiEnabled)
    builder.Services.AddWebApiPresentationServices();

var app = builder.Build();

if (presentationType == PresentationType.Console)
    app.Services.GetRequiredService<IConsoleRunner>().Run();

if (isWebApiEnabled)
{
    app.MapControllers();
    app.Run();
}