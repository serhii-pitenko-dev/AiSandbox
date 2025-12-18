using AiSandBox.Domain.Agents.Entities;

namespace AiSandBox.Ai.Messages
{
    public record class AgentActionMessage(Agent Agent, Guid PlaygroundId);
}
