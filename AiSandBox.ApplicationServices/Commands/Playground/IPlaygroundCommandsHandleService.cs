using AiSandBox.ApplicationServices.Commands.Playground.CreatePlayground;

namespace AiSandBox.ApplicationServices.Commands.Playground;

public interface IPlaygroundCommandsHandleService
{
    public ICreatePlaygroundCommandHandler CreatePlaygroundCommand { get; }
}