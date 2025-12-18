using AiSandBox.SharedBaseTypes.ValueObjects;


namespace AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;

public record class AgentMoveActionEvent(
    Guid AgentId,
    Coordinates From, 
    Coordinates To,
    bool IsSuccess) : BaseAgentActionEvent(AgentId, EAction.Run, IsSuccess);

