using AiSandBox.Domain.Agents.Entities;
using AiSandBox.Domain.Maps;

namespace AiSandBox.Domain.Agents.Factories;

public interface IEnemyFactory
{
    Enemy CreateEnemy(Cell cell, InitialAgentCharacters characters);
}

