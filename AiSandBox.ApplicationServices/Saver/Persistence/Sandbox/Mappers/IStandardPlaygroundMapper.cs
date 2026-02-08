using AiSandBox.ApplicationServices.Saver.Persistence.Sandbox.States;
using AiSandBox.Domain.Playgrounds;

namespace AiSandBox.ApplicationServices.Saver.Persistence.Sandbox.Mappers;

public interface IStandardPlaygroundMapper
{
    StandardPlaygroundState ToState(StandardPlayground playground);
    StandardPlayground FromState(StandardPlaygroundState state);
}
