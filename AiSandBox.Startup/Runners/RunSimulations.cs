using AiSandBox.ApplicationServices.Runner;
using AiSandBox.Domain.Statistics.Result;
using AiSandBox.Infrastructure.FileManager;

namespace AiSandBox.Startup.Runners;

public class RunSimulations
{
    private readonly IFileDataManager<GeneralBatchRunInformation> _batchResultFileManager;

    public RunSimulations(IFileDataManager<GeneralBatchRunInformation> batchResultFileManager)
    {
        _batchResultFileManager = batchResultFileManager;
    }

    public async Task RunSingleAsync(IExecutorForPresentation executor)
        => await executor.RunAsync();

    public async Task RunSingleTrainedAsync(IStandardExecutor executor)
        => await executor.RunAsync();

    public async Task RunTestPreconditionsAsync(IExecutorForPresentation executor)
        => await executor.TestRunWithPreconditionsAsync();

    public async Task RunManyAsync(IExecutorForPresentation executor, int count)
    {
        var batchRunId = Guid.NewGuid();
        int wins = 0;
        int completedRuns = 0;
        long totalTurns = 0;
        int firstRunClaimed = 0;

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount
        };

        await Parallel.ForEachAsync(
            Enumerable.Range(0, count),
            options,
            async (_, _) =>
            {
                var result = await executor.RunAndCaptureAsync();

                // Write GeneralBatchRunInformation once — from whichever run finishes first.
                if (Interlocked.Exchange(ref firstRunClaimed, 1) == 0)
                    await _batchResultFileManager.AppendObjectAsync(batchRunId, result.MapInfo);

                await _batchResultFileManager.AppendObjectAsync(batchRunId, result.Run);

                Interlocked.Increment(ref completedRuns);
                Interlocked.Add(ref totalTurns, result.Run.TurnsCount);
                if (result.Run.WinReason.HasValue)
                    Interlocked.Increment(ref wins);
            });

        int losses = completedRuns - wins;
        double avgTurns = completedRuns > 0 ? (double)totalTurns / completedRuns : 0;
        await _batchResultFileManager.AppendObjectAsync(batchRunId, new BatchSummary(completedRuns, wins, losses, avgTurns));
    }
}
