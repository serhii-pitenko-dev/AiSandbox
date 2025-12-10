using AiSandBox.SharedBaseTypes.GlobalEvents;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Runner;

public interface IExecutor
{
    event Action<Guid>? GameStarted;
    event Action<Guid>? TurnExecuted;
    event Action<Guid, ESandboxStatus>? ExecutionFinished;
    event Action<Guid>? MapObjectChanged;
    event Action<Guid, GlobalEvent>? OnGlobalEvent;

    void Run();
}