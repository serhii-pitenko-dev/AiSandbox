using AiSandBox.Domain.Statistics.Result;

namespace AiSandBox.ApplicationServices.Runner;

public interface IExecutorForPresentation : IExecutor
{
    /// <summary>
    /// Runs a single simulation and returns the outcome (map config + run result).
    /// No batch awareness — the caller decides what to do with the result.
    /// </summary>
    Task<SandboxRunResult> RunAndCaptureAsync();
}

