using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Common.MessageBroker.Contracts.CoreServicesContract.Events;

public record OnAgentToggleActionEvent(
    Guid Id, 
    Guid PlaygroundId, 
    Guid AgentId,
    AgentAction AgentAction,
    bool IsActivated,
    AgentSnapshot AgentSnapshot) : OnBaseAgentActionEvent(Id, PlaygroundId, AgentId, AgentSnapshot);

