using AiSandBox.SharedBaseTypes.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSandBox.Ai.AgentActions;

public class RandomActionsForSimulation : RandomActions
{
    protected override void ApplyAgentActionEvent(Guid agentId, bool isActivated, EAction action)
    {
        // No implementation needed for simulation
    }

    protected override void ApplyAgentMoveActionEvent(Guid agentId, Coordinates from, Coordinates to)
    {
        // No implementation needed for simulation
    }
}