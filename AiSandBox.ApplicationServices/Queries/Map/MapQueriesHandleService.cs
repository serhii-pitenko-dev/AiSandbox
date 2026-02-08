using AiSandBox.ApplicationServices.Queries.Maps.GetAffectedCells;
using AiSandBox.ApplicationServices.Queries.Maps.GetMapLayout;

namespace AiSandBox.ApplicationServices.Queries.Maps;

public class MapQueriesHandleService(
    IMapLayout mapLayoutQuery,
    IAffectedCells affectedCellsQuery) : IMapQueriesHandleService
{
    public required IMapLayout MapLayoutQuery { get; set; } = mapLayoutQuery;
    public required IAffectedCells AffectedCellsQuery { get; set; } = affectedCellsQuery;
}