using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;
using System.Text.Json.Serialization;

namespace AiSandBox.Domain;

public abstract class SandboxMapBaseObject
{
    private Cell? Cell { get; set; }

    public ObjectType Type { get; protected set; }
    public Guid Id { get; private set; }

    /// <summary>
    /// Object's coordinates on the map
    /// </summary>
    public Coordinates Coordinates 
    {
        get
        {
            return Cell != null ? Cell.Coordinates : throw new InvalidOperationException("Cell is not assigned.");
        }
    }
    
    public bool Transparent { get; protected set; }

    // Parameterless constructor for deserialization
    protected SandboxMapBaseObject()
    {
    }

    // Primary constructor
    protected SandboxMapBaseObject(ObjectType type, Cell? cell, Guid id)
    {
        Type = type;
        Cell = cell;
        Id = id;
    }

    public virtual void UpdateCell(Cell cell)
    {
        Cell = cell;
    }

    public void CopyTo(SandboxMapBaseObject target)
    {
        target.Type = Type;
        target.Id = Id;
        target.Transparent = Transparent;
        target.Cell = Cell;
    }
}
