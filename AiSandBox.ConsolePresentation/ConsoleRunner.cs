using AiSandBox.ApplicationServices.Orchestrators;
using AiSandBox.ApplicationServices.Queries.Map.Entities;
using AiSandBox.ApplicationServices.Queries.Maps;
using AiSandBox.ApplicationServices.Queries.Maps.GetMapInitialPeconditions;
using AiSandBox.ApplicationServices.Queries.Maps.GetMapLayout;
using AiSandBox.ApplicationServices.Runner;
using AiSandBox.ConsolePresentation.Settings;
using AiSandBox.SharedBaseTypes.GlobalEvents;
using AiSandBox.SharedBaseTypes.GlobalEvents.Actions.Agent;
using AiSandBox.SharedBaseTypes.ValueObjects;
using ConsolePresentation;
using Microsoft.Extensions.Options;
using Spectre.Console;

namespace AiSandBox.ConsolePresentation;

public class ConsoleRunner : IConsoleRunner
{
    private readonly IExecutor _executor;
    private readonly IMapQueriesHandleService _mapQueries;
    private readonly ConsoleSize _consoleSize;
    private readonly ColorScheme _consoleColorScheme;
    private readonly int _movementTimeout;
    private Dictionary<EObjectType, string> _cellData = [];
    private Guid _playgroundId;
    private PreconditionsResponse? _preconditionsResponse;
    private int _mapRenderStartRow = 7; // Row where map rendering starts
    private int _mapWidth;
    private int _mapHeight;
    private List<string> _eventMessages = [];
    public event Action<Guid>? ReadyForRendering;


    public ConsoleRunner(
        IExecutor executor,
        IMapQueriesHandleService mapQueries,
        IOptions<ConsoleSettings> consoleSettings,
        ITurnFinalizator turnFinalizator)
    {
        _executor = executor;
        _mapQueries = mapQueries;
        _consoleSize = consoleSettings.Value.ConsoleSize;
        _consoleColorScheme = consoleSettings.Value.ColorScheme;
        _movementTimeout = consoleSettings.Value.MovementTimeout;
    }

    public void Run()
    {
        // Initialize console
        InitializeConsole();

        // Subscribe to executor events
        _executor.GameStarted += OnGameStarted;
        _executor.MapObjectChanged += OnMapObjectChanged;
        _executor.TurnExecuted += OnTurnEnded;
        _executor.ExecutionFinished += OnExecutionFinished;
        _executor.OnGlobalEvent += OnGlobalEventHandler;

        // Start the game (business logic in executor)
        _executor.Run();

        // Cleanup
        _executor.GameStarted -= OnGameStarted;
        _executor.MapObjectChanged -= OnMapObjectChanged;
        _executor.TurnExecuted -= OnTurnEnded;
        _executor.ExecutionFinished -= OnExecutionFinished;
        _executor.OnGlobalEvent -= OnGlobalEventHandler;

        Console.ReadLine();
    }

    private void InitializeConsole()
    {
        Console.CursorVisible = false; // Hide the cursor
        InitializeElementsRendering();
        ResizeConsole(_consoleSize.Width, _consoleSize.Height);
        RenderBackground(_consoleColorScheme.GlobalBackGroundColor);
    }

    private void OnGameStarted(Guid playgroundId)
    {
        _playgroundId = playgroundId;
        RenderInitialGameInfo();

        // Render the full map at game beginning
        MapLayoutResponse fullMapLayout = _mapQueries.MapLayoutQuery.GetFromMemory(_playgroundId);
        _mapWidth = fullMapLayout.Cells.GetLength(0);
        _mapHeight = fullMapLayout.Cells.GetLength(1);
        RenderFullMap(fullMapLayout);
    }

    private void OnMapObjectChanged(Guid objectId)
    {
        Task.Delay(_movementTimeout).Wait();
        // Get only the affected cells for the changed object
        MapLayoutResponse affectedCells = _mapQueries.MapLayoutQuery.GetObjectAffectedCellsFromMemory(_playgroundId, objectId);

        // Update only the affected cells on the console
        UpdateAffectedCells(affectedCells);
    }

    private void OnTurnEnded(Guid playgroundId)
    {
        // Clear event messages for the new turn
        _eventMessages.Clear();
        RenderBottomData();
    }

    private void OnGlobalEventHandler(Guid playgroundId, GlobalEvent globalEvent)
    {
        string eventMessage = ConvertEventToString(globalEvent);
        _eventMessages.Add(eventMessage);
        RenderBottomData();
    }

    private string ConvertEventToString(GlobalEvent globalEvent)
    {
        return globalEvent switch
        {
            AgentMoveActionEvent moveEvent =>
                $"Agent {moveEvent.AgentId:N} moved from ({moveEvent.From.X}, {moveEvent.From.Y}) to ({moveEvent.To.X}, {moveEvent.To.Y})",
            AgentActionEvent actionEvent =>
                $"Agent {actionEvent.AgentId:N} {(actionEvent.IsActivated ? "activated" : "deactivated")} action: {actionEvent.ActionType}",
            BaseAgentActionEvent baseActionEvent =>
                $"Agent {baseActionEvent.AgentId:N} performed action: {baseActionEvent.ActionType}",
            _ => $"Unknown event: {globalEvent.GetType().Name}"
        };
    }

    private void RenderInitialGameInfo()
    {
        _preconditionsResponse = _mapQueries.MapInitialPreconditionsQuery.Get(_playgroundId);

        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        WriteSysInfoLine($"Map initialized with ID: {_playgroundId}");
        WriteSysInfoLine($"Width: {_preconditionsResponse.Width}; Height: {_preconditionsResponse.Height}; Area: {_preconditionsResponse.Area}; Percent of blocks: {_preconditionsResponse.PercentOfBlocks}; Percent of enemies {_preconditionsResponse.PercentOfEnemies}");
        WriteSysInfoLine($"Initial elements count: blocks - {_preconditionsResponse.BlocksCount}; enemy - {_preconditionsResponse.EnemiesCount}");
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
    }

    private void RenderFullMap(MapLayoutResponse mapRenderData)
    {
        int width = mapRenderData.Cells.GetLength(0);
        int height = mapRenderData.Cells.GetLength(1);

        Console.SetCursorPosition(0, 6);
        WriteSysInfoLine($" Turn: {mapRenderData.turnNumber}");

        // Render top border
        AnsiConsole.MarkupLine($"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.MapBackGroundColor}] {new string('█', width + 2)}[/]");

        // Render rows: iterate Cartesian Y from top to bottom
        for (int cartesianY = height - 1; cartesianY >= 0; cartesianY--)
        {
            string row = string.Empty;

            for (int x = 0; x < width; x++)
            {
                MapCell cell = mapRenderData.Cells[x, cartesianY];
                row += GetCellSymbol(cell);
            }

            string leftBorder = (cartesianY < 10) ? cartesianY.ToString() : "█";

            AnsiConsole.MarkupLine(
                $"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.MapBackGroundColor}] {leftBorder}[/]" +
                $"{row}" +
                $"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.MapBackGroundColor}]█[/]");
        }

        // Render bottom border
        string bottomBorder = " █";
        for (int x = 0; x < width; x++)
        {
            bottomBorder += (x < 10) ? x.ToString() : "█";
        }
        bottomBorder += "█";

        AnsiConsole.MarkupLine($"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.MapBackGroundColor}]{bottomBorder}[/]");

        RenderBottomData();
    }

    private void UpdateAffectedCells(MapLayoutResponse affectedCellsLayout)
    {
        int width = affectedCellsLayout.Cells.GetLength(0);
        int height = affectedCellsLayout.Cells.GetLength(1);

        for (int cartesianY = 0; cartesianY < height; cartesianY++)
        {
            for (int x = 0; x < width; x++)
            {
                MapCell cell = affectedCellsLayout.Cells[x, cartesianY];

                // Convert Cartesian to screen coordinates for rendering
                int screenRow = _mapRenderStartRow + 1 + (_mapHeight - 1 - cartesianY);
                int screenCol = 2 + x;

                Console.SetCursorPosition(screenCol, screenRow);
                AnsiConsole.Markup(GetCellSymbol(cell));
            }
        }

        Console.SetCursorPosition(0, _mapRenderStartRow + _mapHeight + 3);
    }

    private string GetCellSymbol(MapCell cell)
    {
        // First check if there's an actual agent/object at this cell
        if (cell.ObjectType != EObjectType.Empty)
        {
            return _cellData[cell.ObjectType];
        }

        // Then check for effects on empty cells
        foreach (var agentEffect in cell.Effects)
        {
            // Check hero effects first (higher priority)
            if (agentEffect.AgentType == EObjectType.Hero)
            {
                if (agentEffect.Effects.Contains(EEffect.Path))
                {
                    return $"[#000000 on {_consoleColorScheme.HeroPathColor}]·[/]";
                }
                else if (agentEffect.Effects.Contains(EEffect.Vision))
                {
                    return $"[#000000 on {_consoleColorScheme.HeroVisionColor}]·[/]";
                }
            }
            // Check enemy effects
            else if (agentEffect.AgentType == EObjectType.Enemy)
            {
                if (agentEffect.Effects.Contains(EEffect.Path))
                {
                    return $"[#000000 on {_consoleColorScheme.EnemyPathColor}]·[/]";
                }
                else if (agentEffect.Effects.Contains(EEffect.Vision))
                {
                    return $"[#000000 on {_consoleColorScheme.EnemyVisionColor}]·[/]";
                }
            }
        }

        return _cellData[EObjectType.Empty];
    }

    private void RenderBottomData()
    {
        // Calculate the starting row for bottom data (after the map)
        int bottomDataStartRow = _mapRenderStartRow + _mapHeight + 3;

        Console.SetCursorPosition(0, bottomDataStartRow);

        // Clear the bottom area with empty lines
        for (int i = 0; i < 6; i++)
        {
            AnsiConsole.MarkupLine($"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.GlobalBackGroundColor}]{new string(' ', Console.WindowWidth)}[/]");
        }

        // Render event messages
        Console.SetCursorPosition(0, bottomDataStartRow);
        AnsiConsole.MarkupLine($"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.GlobalBackGroundColor}]Events:[/]");

        // Display the last 5 events
        int eventsToShow = Math.Min(_eventMessages.Count, 5);
        for (int i = _eventMessages.Count - eventsToShow; i < _eventMessages.Count; i++)
        {
            AnsiConsole.MarkupLine($"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.GlobalBackGroundColor}]  {_eventMessages[i]}[/]");
        }
    }

    private void WriteSysInfoLine(string message)
    {
        AnsiConsole.MarkupLine($"[{_consoleColorScheme.BorderColor} on {_consoleColorScheme.GlobalBackGroundColor}]{message}[/]");
    }

    private void InitializeElementsRendering()
    {
        _cellData = new Dictionary<EObjectType, string>
        {
            { EObjectType.Empty, $"[#000000 on {_consoleColorScheme.MapBackGroundColor}]·[/]" },
            { EObjectType.Block, $"[{_consoleColorScheme.BlockColor} on {_consoleColorScheme.MapBackGroundColor}]█[/]" },
            { EObjectType.Hero, $"[{_consoleColorScheme.HeroColor} on {_consoleColorScheme.MapBackGroundColor}]█[/]" },
            { EObjectType.Enemy, $"[{_consoleColorScheme.EnemyColor} on {_consoleColorScheme.MapBackGroundColor}]X[/]" },
            { EObjectType.Exit, $"[Black on Green]E[/]" }
        };
    }

    private static void ResizeConsole(int width, int height)
    {
        // Make sure buffer is at least as big as window
        if (Console.LargestWindowWidth < width)
            width = Console.LargestWindowWidth;

        if (Console.LargestWindowHeight < height)
            height = Console.LargestWindowHeight;

        // First set buffer (>= window)
        Console.SetBufferSize(width, height);

        // Then set window
        Console.SetWindowSize(width, height);
    }

    private static void RenderBackground(string backgroundColor)
    {
        Console.Clear();
        var width = Console.WindowWidth;
        var height = Console.WindowHeight;

        for (int i = 0; i < height; i++)
            AnsiConsole.MarkupLine($"[#000000 on {backgroundColor}]{new string(' ', width)}[/]");

        Console.SetCursorPosition(0, 0);
    }

    private void OnExecutionFinished(Guid playgroundId, ESandboxStatus turnStatus)
    {
        switch (turnStatus)
        {
            case ESandboxStatus.HeroWon:
                OnGameWon();
                break;
            case ESandboxStatus.HeroLost:
                OnGameLost();
                break;
            case ESandboxStatus.TurnLimitReached:
                OnTurnLimitReached();
                break;
            default:
                WriteSysInfoLine($"Unknown game status: {turnStatus}");
                break;
        }
    }

    private void OnGameWon()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[Green on {_consoleColorScheme.GlobalBackGroundColor}]!!! HERO WIN !!![/]");
    }

    private void OnGameLost()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[Red on {_consoleColorScheme.GlobalBackGroundColor}]!!! HERO LOST !!![/]");
    }

    private void OnTurnLimitReached()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[Red on {_consoleColorScheme.GlobalBackGroundColor}]!!! TURN LIMIT REACHED !!![/]");
    }
}