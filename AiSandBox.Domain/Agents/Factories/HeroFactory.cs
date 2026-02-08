using AiSandBox.Domain.Agents.Entities;
using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.Agents.Factories;

public class HeroFactory: IHeroFactory
{
    public Hero CreateHero(InitialAgentCharacters characters)
    {
        return new Hero(characters, Guid.NewGuid());
    }
}

