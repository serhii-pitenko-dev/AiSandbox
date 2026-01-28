using AiSandBox.Common.MessageBroker.MessageTypes;

namespace AiSandBox.Common.MessageBroker.Contracts.GlobalMessagesContract.Events.Win;

public record class HeroWonEvent(Guid id, Guid PlaygroundId, WinReason WinReason): Event(id);
