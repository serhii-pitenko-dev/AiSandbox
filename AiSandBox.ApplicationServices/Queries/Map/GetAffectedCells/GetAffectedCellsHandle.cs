using AiSandBox.ApplicationServices.Converters.Maps;
using AiSandBox.Domain.Playgrounds;
using AiSandBox.Infrastructure.MemoryManager;

namespace AiSandBox.ApplicationServices.Queries.Maps.GetAffectedCells;

public class GetAffectedCellsHandle(IMemoryDataManager<StandardPlayground> memoryDataManager) : IAffectedCells
{
    public AffectedCellsResponse GetFromMemory(Guid playgroundId, Guid objectId)
    {
        StandardPlayground playground = memoryDataManager.LoadObject(playgroundId);

        return playground.GetObjectAffectedCells(objectId);
    }
}