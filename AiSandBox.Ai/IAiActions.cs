using AiSandBox.Domain.Agents.Entities;
using AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;
using AiSandBox.SharedBaseTypes.GlobalEvents.GameStateEvents;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Ai.AgentActions;

public interface IAiActions
{
    event Action<GameLostEvent>? OnGameLost;
    event Action<GameWonEvent>? OnGameWin;
    event Action<BaseAgentActionEvent>? OnAgentAction;
    event Action<List<BaseAgentActionEvent>>? OnAgentActionsCompleted;
}

