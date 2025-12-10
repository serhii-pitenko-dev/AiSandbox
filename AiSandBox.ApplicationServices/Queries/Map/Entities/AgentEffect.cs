using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Queries.Map.Entities;

public record struct AgentEffect(Guid AgentId, EObjectType AgentType, EEffect[] Effects);


