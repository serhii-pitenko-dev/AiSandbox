namespace AiSandBox.SharedBaseTypes.MessageTypes;

public record Response(Guid Id, Guid CorrelationId) : Message(Id);

