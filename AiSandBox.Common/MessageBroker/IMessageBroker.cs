namespace AiSandBox.Common.MessageBroker;

public interface IMessageBroker
{
    void Publish<TMessage>(TMessage message) where TMessage : class;
    void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : class;
    void Unsubscribe<TMessage>(Action<TMessage> handler) where TMessage : class;
}