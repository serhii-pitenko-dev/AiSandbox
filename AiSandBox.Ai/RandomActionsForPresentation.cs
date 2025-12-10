using AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Ai.AgentActions;

public class RandomActionsForPresentation : RandomActions
{
    protected override void ApplyAgentActionEvent(Guid agentId, bool isActivated, EAction action)
    {
        RaiseAgentAction(new AgentActionEvent(agentId, isActivated, action));
    }

    protected override void ApplyAgentMoveActionEvent(Guid agentId, Coordinates from, Coordinates to)
    {
        RaiseAgentMoveAction(new AgentMoveActionEvent(agentId, from, to));
    }
}

