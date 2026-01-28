using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Common.MessageBroker.Contracts.AiContract.Responses;

/// <summary>
/// Event representing agent movement from one position to another
/// </summary>
public record AgentDecisionMoveResponse(
    Guid Id,
    Guid AgentId,
    Coordinates From,
    Coordinates To, 
    Guid CorrelationId,
    bool IsSuccess) : AgentDecisionBaseResponse(Id, AgentId, AgentAction.Move, CorrelationId, IsSuccess);