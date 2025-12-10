using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;

public record class AgentActionEvent(
    Guid AgentId, 
    bool IsActivated, 
    EAction ActionType) : BaseAgentActionEvent(AgentId, ActionType);



