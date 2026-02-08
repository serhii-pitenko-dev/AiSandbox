using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Saver.Persistence.Sandbox.States;

public record BlockState
{
    public Guid Id { get; init; }
    public Coordinates Coordinates { get; init; }
}