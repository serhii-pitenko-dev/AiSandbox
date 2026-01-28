namespace AiSandBox.SharedBaseTypes.MessageTypes;

public record Command(Guid Id) : Message(Id);