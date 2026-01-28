using AiSandBox.Common.MessageBroker.MessageTypes;

namespace AiSandBox.Common.MessageBroker.Contracts.GlobalMessagesContract.Events.Lost;

public record HeroLostEvent(Guid id, Guid PlaygroundId, LostReason LostReason): Event(id);