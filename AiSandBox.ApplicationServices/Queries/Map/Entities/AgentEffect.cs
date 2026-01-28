using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Queries.Map.Entities;

public record struct AgentEffect(Guid AgentId, ObjectType AgentType, EEffect[] Effects);


