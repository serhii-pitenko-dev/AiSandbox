using AiSandBox.Domain.Agents.Entities;
using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.UnitTests.AiSandBox.Domain.Agents.Entities.AgentTest;

/// <summary>
/// TODO: Review all tests and add tests for deactivating Run ability and its effects on stamina regeneration.
/// </summary>
[TestClass]
public class AgentTestRunAbility
{
    /// <summary>
    /// Test helper class that inherits from abstract Agent to enable testing.
    /// Provides access to protected members for test verification.
    /// </summary>
    private class TestAgent : Agent
    {
        public TestAgent(Cell cell, InitialAgentCharacters characters, Guid id)
            : base(ObjectType.Hero, characters, cell, id)
        {
        }

        public TestAgent() : base()
        {
        }

        public void SetAvailableLimitedActions(List<AgentAction> actions)
        {
            AvailableActions = actions;
        }

        public List<AgentAction> GetAvailableLimitedActions()
        {
            return AvailableActions;
        }

        public List<AgentAction> GetExecutedActions()
        {
            return ExecutedActions;
        }
    }

    /// <summary>
    /// Tests the Run method with various stamina scenarios to verify correct movement calculation.
    /// Validates that movements are doubled when stamina allows, or limited by available stamina.
    /// Test parameters: speed, stamina, initialMoveCount, expectedMoveCount.
    /// </summary>
    /// <param name="speed">Agent's speed value</param>
    /// <param name="stamina">Agent's available stamina</param>
    /// <param name="initialMoveCount">Number of Move actions available before Run</param>
    /// <param name="expectedMoveCount">Expected number of Move actions after Run</param>
    [TestMethod]
    [DataRow(5, 10, 5, 10, DisplayName = "WithSufficientStamina_DoublesAvailableMovements")]
    [DataRow(5, 6, 5, 9, DisplayName = "WithInsufficientStamina_AddsPartialMovements")]
    [DataRow(5, 0, 0, 0, DisplayName = "WithZeroMovements_DoublesCorrectly")]
    [DataRow(5, 8, 4, 8, DisplayName = "WithExactStaminaForDoubling_AddsCorrectMovements")]
    [DataRow(5, 2, 3, 5, DisplayName = "WithLowStamina_AddsMinimalMovements")]
    [DataRow(3, 20, 8, 16, DisplayName = "WithHighStamina_DoublesLargeMovementCount")]
    [DataRow(5, 1, 2, 3, DisplayName = "WithVeryLowStamina_AddsOnlyOneMovement")]
    public void Run_VariousStaminaScenarios_AddsCorrectMovements(
        int speed,
        int stamina,
        int initialMoveCount,
        int expectedMoveCount)
    {
        // Arrange
        var cell = new Cell(new Coordinates(0, 0));
        var characters = new InitialAgentCharacters(speed, sightRange: 3, stamina);
        var agent = new TestAgent(cell, characters, Guid.NewGuid());

        var initialActions = Enumerable.Repeat(AgentAction.Move, initialMoveCount).ToList();
        agent.SetAvailableLimitedActions(initialActions);

        // Act
        agent.DoAction(AgentAction.Run, true);

        // Assert
        var moveCount = agent.GetAvailableLimitedActions().Count(a => a == AgentAction.Move);
        Assert.AreEqual(expectedMoveCount, moveCount,
            $"Expected {expectedMoveCount} movements with stamina {stamina} and {initialMoveCount} initial moves");
    }

    /// <summary>
    /// Verifies that calling Run sets the IsRun property to true,
    /// indicating that the run ability has been activated.
    /// </summary>
    [TestMethod]
    public void Run_SetsIsRunToTrue()
    {
        // Arrange
        var cell = new Cell(new Coordinates(0, 0));
        var characters = new InitialAgentCharacters(speed: 5, sightRange: 3, stamina: 10);
        var agent = new TestAgent(cell, characters, Guid.NewGuid());
        agent.SetAvailableLimitedActions([AgentAction.Move]);

        // Act
        agent.DoAction(AgentAction.Run, true);

        // Assert
        Assert.IsTrue(agent.IsRun, "IsRun property should be set to true");
    }

    /// <summary>
    /// Verifies that calling Run adds the AgentAction.Run action to the ExecutedActions list,
    /// tracking that the run ability has been used.
    /// </summary>
    [TestMethod]
    public void Run_AddsRunActionToExecutedActions()
    {
        // Arrange
        var cell = new Cell(new Coordinates(0, 0));
        var characters = new InitialAgentCharacters(speed: 5, sightRange: 3, stamina: 10);
        var agent = new TestAgent(cell, characters, Guid.NewGuid());
        agent.SetAvailableLimitedActions([AgentAction.Move, AgentAction.Move]);

        // Act
        agent.DoAction(AgentAction.Run, true);

        // Assert
        var executedActions = agent.GetExecutedActions();
        Assert.AreEqual(1, executedActions.Count, "ExecutedActions should have exactly one action");
        Assert.AreEqual(AgentAction.Run, executedActions[0], "The executed action should be Run");
    }

}