using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.InanimateObjects;

public class Block: SandboxMapBaseObject
{
    public Block(Cell cell, Guid id) : base(ObjectType.Block, cell, id) 
    { 
        Transparent = false;
    }
}