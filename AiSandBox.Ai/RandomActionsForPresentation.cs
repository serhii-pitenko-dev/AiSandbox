using AiSandBox.Common.MessageBroker;
using AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Ai.AgentActions;

public class RandomActionsForPresentation : RandomActions
{
    public RandomActionsForPresentation(IMessageBroker messageBroker) : base(messageBroker)
    {
    }

    protected override void ApplyAgentActionEvent(BaseAgentActionEvent agentEvent)
    {
        RaiseAgentAction(agentEvent);
    }
}

