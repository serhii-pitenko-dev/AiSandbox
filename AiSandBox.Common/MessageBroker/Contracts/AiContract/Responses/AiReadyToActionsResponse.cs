using AiSandBox.SharedBaseTypes.MessageTypes;

namespace AiSandBox.Common.MessageBroker.Contracts.AiContract.Responses;

public record AiReadyToActionsResponse(Guid Id, Guid PlaygroundId, Guid CorrelationId): Response(Id, CorrelationId);