using AiSandBox.Common.MessageBroker.Contracts.GlobalMessagesContract.Events;
using AiSandBox.SharedBaseTypes.ValueObjects;
using AiSandBox.SharedBaseTypes.MessageTypes;

namespace AiSandBox.Common.MessageBroker.Contracts.AiContract.Responses;

public record class AgentDecisionBaseResponse(
    Guid Id, 
    Guid AgentId, 
    AgentAction ActionType, 
    Guid CorrelationId,
    bool IsSuccess): Response(Id, CorrelationId);