using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.SharedBaseTypes.AiContract.Dto;

public record AgentStateForAIDecision(
    Guid PlaygroundId,
    Guid Id,
    ObjectType Type,
    Coordinates Coordinates,
    int Speed,
    int SightRange,
    bool IsRun,
    int Stamina,
    int MaxStamina,
    List<VisibleCellData> VisibleCells,
    List<AgentAction> AvailableLimitedActions,
    List<AgentAction> ExecutedActions);


