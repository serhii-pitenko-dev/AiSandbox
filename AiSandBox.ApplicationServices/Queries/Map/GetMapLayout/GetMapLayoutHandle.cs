using AiSandBox.ApplicationServices.Converters.Maps;
using AiSandBox.Domain.Playgrounds;
using AiSandBox.Infrastructure.FileManager;
using AiSandBox.Infrastructure.MemoryManager;

namespace AiSandBox.ApplicationServices.Queries.Maps.GetMapLayout;

public class GetMapLayoutHandle(
    IMemoryDataManager<StandardPlayground> MemoryDataManager,
    IFileDataManager<StandardPlayground> FileDataManager) : IMapLayout
{
    public MapLayoutResponse GetFromMemory(Guid guid)
    {
        StandardPlayground playground = MemoryDataManager.LoadObject(guid);

        return playground.ToMapLayout();
    }

    public async Task<MapLayoutResponse> GetFromFile(Guid guid)
    {
        StandardPlayground playground = await FileDataManager.LoadObjectAsync(guid);
        MemoryDataManager.AddOrUpdate(guid, playground);

        return playground.ToMapLayout();
    }
}