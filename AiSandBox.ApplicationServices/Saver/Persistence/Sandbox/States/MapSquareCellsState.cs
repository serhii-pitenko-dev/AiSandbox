namespace AiSandBox.ApplicationServices.Saver.Persistence.Sandbox.States;

public record MapSquareCellsState
{
    public int Width { get; init; }
    public int Height { get; init; }
    public CellState[,] CellGrid { get; init; } = null!;
}