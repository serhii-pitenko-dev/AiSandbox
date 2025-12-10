using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;

public record class BaseAgentActionEvent(Guid AgentId, EAction ActionType): GlobalEvent;