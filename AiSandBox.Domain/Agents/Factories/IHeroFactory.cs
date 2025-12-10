using AiSandBox.Domain.Agents.Entities;
using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.Agents.Factories;

public interface IHeroFactory
{
    Hero CreateHero(Cell cell, InitialAgentCharacters characters);
}

