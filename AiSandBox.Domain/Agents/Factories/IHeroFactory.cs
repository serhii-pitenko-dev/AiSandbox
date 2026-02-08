using AiSandBox.Domain.Agents.Entities;
using AiSandBox.Domain.Maps;

namespace AiSandBox.Domain.Agents.Factories;

public interface IHeroFactory
{
    Hero CreateHero(InitialAgentCharacters characters);
}

