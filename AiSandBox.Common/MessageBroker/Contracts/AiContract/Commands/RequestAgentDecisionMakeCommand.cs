using AiSandBox.SharedBaseTypes.MessageTypes;

namespace AiSandBox.Common.MessageBroker.Contracts.AiContract.Commands;

public record RequestAgentDecisionMakeCommand(
    Guid Id,
    Guid PlaygroundId,
    Guid AgentId) : Command(Id);