using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Queries.Map.Entities;

public record struct MapCell(
    Coordinates Coordinates,
    Guid ObjectId,
    ObjectType ObjectType,
    AgentEffect[] Effects);