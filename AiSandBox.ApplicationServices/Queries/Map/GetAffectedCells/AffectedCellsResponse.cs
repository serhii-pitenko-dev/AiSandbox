using AiSandBox.ApplicationServices.Queries.Map.Entities;

namespace AiSandBox.ApplicationServices.Queries.Maps.GetAffectedCells;

public record AffectedCellsResponse(int TurnNumber, List<MapCell> Cells);