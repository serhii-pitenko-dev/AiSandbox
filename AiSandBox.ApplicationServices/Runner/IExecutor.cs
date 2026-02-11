using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Runner;

public interface IExecutor
{
    Task TestRunWithPreconditionsAsync();

    Task RunAsync(Guid sandboxId = default);
}