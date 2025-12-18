using System.Collections.Concurrent;

namespace AiSandBox.Common.MessageBroker;

public class MessageBroker : IMessageBroker
{
    private readonly ConcurrentDictionary<Type, List<Delegate>> _subscribers = new();

    public void Publish<TMessage>(TMessage message) where TMessage : class
    {
        if (message == null) throw new ArgumentNullException(nameof(message));

        var messageType = typeof(TMessage);
        if (_subscribers.TryGetValue(messageType, out var handlers))
        {
            lock (handlers)
            {
                foreach (var handler in handlers.ToList())
                {
                    ((Action<TMessage>)handler).Invoke(message);
                }
            }
        }
    }

    public void Subscribe<TMessage>(Action<TMessage> handler) where TMessage : class
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        var messageType = typeof(TMessage);
        var handlers = _subscribers.GetOrAdd(messageType, _ => new List<Delegate>());
        
        lock (handlers)
        {
            handlers.Add(handler);
        }
    }

    public void Unsubscribe<TMessage>(Action<TMessage> handler) where TMessage : class
    {
        if (handler == null) throw new ArgumentNullException(nameof(handler));

        var messageType = typeof(TMessage);
        if (_subscribers.TryGetValue(messageType, out var handlers))
        {
            lock (handlers)
            {
                handlers.Remove(handler);
            }
        }
    }
}