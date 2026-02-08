using AiSandBox.ApplicationServices.Commands.Playground.CreatePlayground;

namespace AiSandBox.ApplicationServices.Commands.Playground;

public class PlaygroundCommandsHandleService(
    ICreatePlaygroundCommandHandler createMapCommandHandler
    ) : IPlaygroundCommandsHandleService
{
    public ICreatePlaygroundCommandHandler CreatePlaygroundCommand { get; } = createMapCommandHandler;
}

