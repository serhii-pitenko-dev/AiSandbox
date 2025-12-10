using AiSandBox.SharedBaseTypes.ValueObjects;


namespace AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;

public record class AgentMoveActionEvent(
    Guid AgentId,
    Coordinates From, 
    Coordinates To): BaseAgentActionEvent(AgentId, EAction.Run);

