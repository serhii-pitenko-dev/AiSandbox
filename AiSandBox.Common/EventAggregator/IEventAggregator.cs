namespace AiSandBox.Common.EventAggregator;

/// <summary>
/// Central event aggregator for application-wide event distribution.
/// Provides decoupled communication between components across different layers.
/// </summary>
public interface IEventAggregator
{
    void Publish<TEvent>(TEvent eventData) where TEvent : class;
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
    void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : class;
}