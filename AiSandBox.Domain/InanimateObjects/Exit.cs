using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.InanimateObjects;

public class Exit : SandboxMapBaseObject
{
    public Exit(Cell cell, Guid id) : base(EObjectType.Exit, cell, id) 
    { 
        Transparent = true;
    }
}

