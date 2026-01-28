using AiSandBox.SharedBaseTypes.MessageTypes;

namespace AiSandBox.Common.MessageBroker.MessageTypes;

public record Event(Guid Id) : Message(Id);