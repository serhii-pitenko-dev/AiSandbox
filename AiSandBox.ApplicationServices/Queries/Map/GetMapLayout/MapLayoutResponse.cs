
using AiSandBox.ApplicationServices.Queries.Map.Entities;

namespace AiSandBox.ApplicationServices.Queries.Maps.GetMapLayout;

public record MapLayoutResponse(int turnNumber, MapCell[,] Cells);