using AiSandBox.Common.MessageBroker;
using AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;
using AiSandBox.SharedBaseTypes.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSandBox.Ai.AgentActions;

public class RandomActionsForSimulation : RandomActions
{
    public RandomActionsForSimulation(IMessageBroker messageBroker) : base(messageBroker)
    {
    }

    protected override void ApplyAgentActionEvent(BaseAgentActionEvent agentEvent)
    {
        // In simulation mode, we do not raise the event to avoid side effects
    }
}