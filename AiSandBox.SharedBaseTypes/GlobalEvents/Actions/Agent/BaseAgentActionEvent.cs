using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;

/// <summary>
/// IsSuccess indicates whether the action was successfully executed (true) or failed (false, e.g., due to invalid conditions)
/// </summary>
public record class BaseAgentActionEvent(Guid AgentId, EAction ActionType, bool IsSuccess): GlobalEvent;