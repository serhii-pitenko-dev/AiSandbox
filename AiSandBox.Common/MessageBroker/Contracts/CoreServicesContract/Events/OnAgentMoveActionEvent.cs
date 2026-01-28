using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Common.MessageBroker.Contracts.CoreServicesContract.Events;

public record OnAgentMoveActionEvent(
    Guid Id, 
    Guid PlaygroundId, 
    Guid AgentId,
    Coordinates From,
    Coordinates To,
    bool IsSuccess,
    AgentSnapshot AgentSnapshot) : OnBaseAgentActionEvent(Id, PlaygroundId, AgentId, AgentSnapshot);
