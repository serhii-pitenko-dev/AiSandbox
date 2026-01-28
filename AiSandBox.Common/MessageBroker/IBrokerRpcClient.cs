using AiSandBox.SharedBaseTypes.MessageTypes;

namespace AiSandBox.Common.MessageBroker;

public interface IBrokerRpcClient
{
    Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : notnull, Message
        where TResponse : notnull, Response;
}

