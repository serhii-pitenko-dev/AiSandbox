using AiSandBox.SharedBaseTypes.MessageTypes;

namespace AiSandBox.Common.MessageBroker;

public interface IMessageBroker
{
    void Publish<TMessage>(TMessage message) where TMessage : Message;
    void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : Message;
    void Unsubscribe<TMessage>(Action<TMessage> handler) where TMessage : Message;
}