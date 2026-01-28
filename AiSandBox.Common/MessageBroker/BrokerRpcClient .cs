using AiSandBox.Common.MessageBroker.MessageTypes;
using AiSandBox.SharedBaseTypes.MessageTypes;
using System.Collections.Concurrent;

namespace AiSandBox.Common.MessageBroker;

public class BrokerRpcClient : IBrokerRpcClient, IDisposable
{
    private readonly IMessageBroker _broker;

    private readonly ConcurrentDictionary<Guid, TaskCompletionSource<Response>> _pending
        = new();

    private readonly IDisposable _sub;

    public BrokerRpcClient(IMessageBroker broker)
    {
        _broker = broker ?? throw new ArgumentNullException(nameof(broker));
        _sub = SubscribeToResponses();
    }
    /// <summary>
    /// Sends a request message and waits for a response asynchronously using a correlation-based RPC pattern.
    /// </summary>
    public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : notnull, Message
        where TResponse : notnull, Response 

    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (request is not TRequest)
            throw new InvalidOperationException($"Request type mismatch. Expected {typeof(TRequest).Name}, got {request.GetType().Name}");

        var correlationId = Guid.NewGuid();
        request = request with { Id = correlationId }; // Using record 'with' expression
        var tcs = new TaskCompletionSource<Response>(TaskCreationOptions.RunContinuationsAsynchronously);

        // Register the TaskCompletionSource so OnResponse can complete it when the correlated response arrives
        _pending[correlationId] = tcs;

        try
        {
            // Publish request with correlation ID
            _broker.Publish(request);

            // Wait for response with timeout support
            using var ctr = cancellationToken.Register(() => tcs.TrySetCanceled(cancellationToken));

            var response = await tcs.Task.ConfigureAwait(false);

            if (response is TResponse typedResponse)
            {
                return typedResponse;
            }

            throw new InvalidOperationException($"Response type mismatch. Expected {typeof(Response).Name}, got {response.GetType().Name}");
        }
        finally
        {
            _pending.TryRemove(correlationId, out _);
        }
    }

    private IDisposable SubscribeToResponses()
    {
        // Subscribe to all responses
        Action<Message> handler = OnResponse;
        _broker.Subscribe(handler);

        return new UnsubscribeToken(_broker, handler);
    }

    private void OnResponse(Message resp)
    {
        if (resp is not Response response)
            throw new InvalidOperationException("Received message is not of type Response.");

        if (resp?.Id == null)
            throw new InvalidOperationException("Response correlation ID is missing.");

        if (_pending.TryRemove(response.Id, out var tcs))
        {
            tcs.SetResult(response); // ← Completes the waiting Task
        }
    }

    public void Dispose()
    {
        _sub?.Dispose();

        // Cancel all pending requests
        foreach (var kvp in _pending)
        {
            kvp.Value.TrySetCanceled();
        }

        _pending.Clear();
    }

    private class UnsubscribeToken : IDisposable
    {
        private readonly IMessageBroker _broker;
        private readonly Action<Message> _handler;

        public UnsubscribeToken(IMessageBroker broker, Action<Message> handler)
        {
            _broker = broker;
            _handler = handler;
        }

        public void Dispose()
        {
            _broker.Unsubscribe(_handler);
        }
    }
}