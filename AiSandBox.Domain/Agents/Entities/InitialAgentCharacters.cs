using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.Agents.Entities;

public record struct InitialAgentCharacters(
    int Speed,
    int SightRange,
    int Stamina,
    List<Coordinates> PathToTarget,
    List<AgentAction> AgentActions,
    List<AgentAction> ExecutedActions,
    bool isRun = false,
    int orderInTurnQueue = 0);



