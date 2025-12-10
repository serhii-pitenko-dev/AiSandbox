using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.Agents.Entities;

public class Hero : Agent
{
    public Hero(Cell cell,
        InitialAgentCharacters characters,
        Guid id) : base(EObjectType.Hero, characters, cell, id)
    {

    }

    public Hero() : base(EObjectType.Hero, new InitialAgentCharacters(),  null, new Guid())
    { }

    public Hero Clone()
    {
        var clone = new Hero();

        CopyBaseTo(clone);

        return clone;
    }
}

