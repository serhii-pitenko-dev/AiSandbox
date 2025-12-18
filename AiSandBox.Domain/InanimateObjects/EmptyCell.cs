using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.InanimateObjects;

public class EmptyCell : SandboxMapBaseObject
{
    public EmptyCell(Cell cell) : base(EObjectType.Empty, cell, Guid.Empty) 
    { 
        Transparent = true;
    }
}