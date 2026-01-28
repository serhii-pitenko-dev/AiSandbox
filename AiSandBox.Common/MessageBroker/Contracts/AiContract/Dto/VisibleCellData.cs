using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.SharedBaseTypes.AiContract.Dto;

public record VisibleCellData(
    Coordinates Coordinates,
    ObjectType ObjectType,
    Guid ObjectId,
    bool IsTransparent);