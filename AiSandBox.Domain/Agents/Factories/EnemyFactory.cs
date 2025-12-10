using AiSandBox.Domain.Agents.Entities;
using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.Agents.Factories;

public class EnemyFactory : IEnemyFactory
{
    public Enemy CreateEnemy(Cell cell, InitialAgentCharacters characters)
    {
        return new Enemy(cell, characters, Guid.NewGuid());
    }
}