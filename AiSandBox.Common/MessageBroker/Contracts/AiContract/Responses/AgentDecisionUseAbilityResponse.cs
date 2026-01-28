using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Common.MessageBroker.Contracts.AiContract.Responses;

public record AgentDecisionUseAbilityResponse(
    Guid Id,
    Guid AgentId,
    bool IsActivated,
    AgentAction ActionType,
    Guid CorrelationId,
    bool IsSuccess) : AgentDecisionBaseResponse(Id, AgentId, ActionType, CorrelationId, IsSuccess);