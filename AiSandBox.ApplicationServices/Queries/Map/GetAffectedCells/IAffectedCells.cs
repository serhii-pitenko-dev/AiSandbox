namespace AiSandBox.ApplicationServices.Queries.Maps.GetAffectedCells;

public interface IAffectedCells
{
    AffectedCellsResponse GetFromMemory(Guid playgroundId, Guid objectId);
}