namespace AiSandBox.Common.EventAggregator;

public sealed class EventAggregator : IEventAggregator
{
    private readonly Dictionary<Type, List<Delegate>> _subscribers = new();
    private readonly object _lock = new();

    public void Publish<TEvent>(TEvent eventData) where TEvent : class
    {
        ArgumentNullException.ThrowIfNull(eventData);

        List<Delegate>? handlers;
        lock (_lock)
        {
            if (!_subscribers.TryGetValue(typeof(TEvent), out handlers))
            {
                return;
            }

            handlers = new List<Delegate>(handlers);
        }

        foreach (Action<TEvent> handler in handlers.Cast<Action<TEvent>>())
        {
            try
            {
                handler(eventData);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error in event handler for {typeof(TEvent).Name}: {ex.Message}");
            }
        }
    }

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class
    {
        ArgumentNullException.ThrowIfNull(handler);

        lock (_lock)
        {
            if (!_subscribers.ContainsKey(typeof(TEvent)))
            {
                _subscribers[typeof(TEvent)] = new List<Delegate>();
            }

            _subscribers[typeof(TEvent)].Add(handler);
        }
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : class
    {
        ArgumentNullException.ThrowIfNull(handler);

        lock (_lock)
        {
            if (_subscribers.TryGetValue(typeof(TEvent), out var handlers))
            {
                handlers.Remove(handler);

                if (handlers.Count == 0)
                {
                    _subscribers.Remove(typeof(TEvent));
                }
            }
        }
    }
}