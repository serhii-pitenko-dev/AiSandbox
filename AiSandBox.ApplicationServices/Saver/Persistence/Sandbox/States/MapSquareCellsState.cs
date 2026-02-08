using AiSandBox.Infrastructure.Converters;
using System.Text.Json.Serialization;

namespace AiSandBox.ApplicationServices.Saver.Persistence.Sandbox.States;

public record MapSquareCellsState
{
    public int Width { get; init; }
    public int Height { get; init; }

    [JsonConverter(typeof(TwoDimensionalArrayConverter<CellState>))]
    public CellState[,] CellGrid { get; init; } = null!;
}