using AiSandBox.Common.MessageBroker;
using AiSandBox.Common.MessageBroker.Contracts.AiContract.Responses;
using AiSandBox.Common.MessageBroker.Contracts.CoreServicesContract.Events;

namespace AiSandBox.UnitTests.AiSandBox.Common.MessageBrokers;

[TestClass]
public class BrokerRpcClientTest
{
    IMessageBroker _messageBroker = new MessageBroker();

    IBrokerRpcClient _brokerRpcClient = default;

    [TestInitialize]
    public void Initialize()
    {
        _brokerRpcClient = new BrokerRpcClient(_messageBroker);
    }

    [TestMethod]
    public async Task BrokerRpcClient_PublishWithResponse_Success()
    {
        _messageBroker.Subscribe<GameStartedEvent>(msg =>
        {
            _messageBroker.Publish(new AiReadyToActionsResponse(Guid.NewGuid(), msg.PlaygroundId, msg.Id));
        });

        var result =
            await _brokerRpcClient.RequestAsync<GameStartedEvent, AiReadyToActionsResponse>(new GameStartedEvent(Guid.NewGuid(), Guid.NewGuid()));
    }

}