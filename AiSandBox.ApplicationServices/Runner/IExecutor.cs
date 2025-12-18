using AiSandBox.SharedBaseTypes.GlobalEvents;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Runner;

public interface IExecutor
{
    event Action<Guid>? OnGameStarted;
    event Action<Guid, int>? OnTurnExecuted;
    event Action<Guid, ESandboxStatus>? OnGameFinished;
    event Action<Guid, GlobalEvent>? OnGlobalEventRaised;

    void Run();
}