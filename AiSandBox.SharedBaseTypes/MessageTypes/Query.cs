namespace AiSandBox.SharedBaseTypes.MessageTypes;

public record Query(Guid Id) : Message(Id);
