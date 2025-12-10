using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.Domain.InanimateObjects;

public class EmptyCell : SandboxMapBaseObject
{
    public EmptyCell(Cell cell, Guid id) : base(EObjectType.Empty, cell, id) 
    { 
        Transparent = true;
    }
}