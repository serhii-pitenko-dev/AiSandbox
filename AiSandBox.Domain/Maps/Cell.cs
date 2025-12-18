using AiSandBox.SharedBaseTypes.ValueObjects;
using System.Text.Json.Serialization;

namespace AiSandBox.Domain.Maps;

public class Cell
{
    [JsonInclude]
    public Coordinates Coordinates { get; init; }

    [JsonIgnore]
    public SandboxMapBaseObject Object { get; private set; }

    public void PlaceObjectToThisCell(SandboxMapBaseObject mapObject)
    {
        Object = mapObject;
        Object.UpdateCell(this);
    }
}

