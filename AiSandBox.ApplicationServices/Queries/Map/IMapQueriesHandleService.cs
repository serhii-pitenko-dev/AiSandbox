using AiSandBox.ApplicationServices.Queries.Maps.GetAffectedCells;
using AiSandBox.ApplicationServices.Queries.Maps.GetMapLayout;

namespace AiSandBox.ApplicationServices.Queries.Maps;

public interface IMapQueriesHandleService
{
    public IMapLayout MapLayoutQuery { get; set; }

    public IAffectedCells AffectedCellsQuery { get; set; }
}