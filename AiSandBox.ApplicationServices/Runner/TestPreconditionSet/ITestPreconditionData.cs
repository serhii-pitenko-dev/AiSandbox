using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Runner.TestPreconditionSet;

public interface ITestPreconditionData
{
    Guid CreatePlaygroundWithPreconditions(
        Coordinates? heroCoordinates = null, 
        List<Coordinates> enemies = null, 
        List<Coordinates> blocks = null);
}

