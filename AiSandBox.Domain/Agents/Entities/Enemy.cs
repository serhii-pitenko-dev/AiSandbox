using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.Agents.Entities;

public class Enemy: Agent
{
    public Enemy(
        Cell cell,
        InitialAgentCharacters characters,
        Guid id) : base(EObjectType.Enemy, characters, cell, id) 
    {

    }

    public Enemy(): base(EObjectType.Enemy, new InitialAgentCharacters(), null, new Guid())
    { }

    public Enemy Clone()
    {
        var clone = new Enemy();

        CopyBaseTo(clone);

        return clone;
    }
}

