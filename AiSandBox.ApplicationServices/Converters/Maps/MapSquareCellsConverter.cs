using AiSandBox.ApplicationServices.Queries.Map.Entities;
using AiSandBox.ApplicationServices.Queries.Maps.GetMapLayout;
using AiSandBox.Domain.Agents.Entities;
using AiSandBox.Domain.Playgrounds;
using AiSandBox.SharedBaseTypes.ValueObjects;

namespace AiSandBox.ApplicationServices.Converters.Maps;

public static class MapSquareCellsConverter
{
    public static MapLayoutResponse ToMapLayout(this StandardPlayground playground)
    {
        var agentEffectsMap = new Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>>();

        // Process all agents (Hero and Enemies)
        ProcessAllAgentsEffects(playground, agentEffectsMap);

        // Build and return the map layout
        return BuildMapLayoutResponse(playground, agentEffectsMap);
    }


    public static MapLayoutResponse GetObjectAffectedCells(this StandardPlayground playground, Guid objectId)
    {
        var agentEffectsMap = new Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>>();

        // Process only the specific agent
        ProcessSingleAgentEffects(playground, objectId, agentEffectsMap);

        // Build and return the map layout
        return BuildMapLayoutResponse(playground, agentEffectsMap);
    }

    private static void ProcessAllAgentsEffects(
        StandardPlayground playground,
        Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>> agentEffectsMap)
    {
        // Process Hero's VisibleCells and PathToTarget
        if (playground.Hero != null)
        {
            ProcessAgentEffects(playground.Hero, agentEffectsMap);
        }

        // Process all Enemies' VisibleCells and PathToTarget
        foreach (var enemy in playground.Enemies)
        {
            ProcessAgentEffects(enemy, agentEffectsMap);
        }
    }

    private static void ProcessSingleAgentEffects(
        StandardPlayground playground,
        Guid objectId,
        Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>> agentEffectsMap)
    {
        // Check if it's the Hero
        if (playground.Hero?.Id == objectId)
        {
            ProcessAgentEffects(playground.Hero, agentEffectsMap);
            return;
        }

        // Check if it's an Enemy
        var enemy = playground.Enemies.FirstOrDefault(e => e.Id == objectId);
        if (enemy != null)
        {
            ProcessAgentEffects(enemy, agentEffectsMap);
        }
    }

    private static void ProcessAgentEffects(
        Agent agent,
        Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>> agentEffectsMap)
    {
        var agentId = agent.Id;

        // Add Vision effects
        foreach (var visibleCell in agent.VisibleCells)
        {
            AddEffectForAgent(agentEffectsMap, visibleCell.Coordinates, agentId, EEffect.Vision);
        }

        // Add Path effects
        foreach (var pathCoordinate in agent.PathToTarget)
        {
            AddEffectForAgent(agentEffectsMap, pathCoordinate, agentId, EEffect.Path);
        }
    }

    private static MapLayoutResponse BuildMapLayoutResponse(
        StandardPlayground playground,
        Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>> agentEffectsMap)
    {
        MapCell[,] cells = new MapCell[playground.MapWidth, playground.MapHeight];

        // Populate the MapCell array
        for (int x = 0; x < playground.MapWidth; x++)
        {
            for (int y = 0; y < playground.MapHeight; y++)
            {
                var cell = playground.GetCell(x, y);
                var coordinates = cell.Coordinates;

                // Convert the effects map to AgentEffect array
                var agentEffects = ConvertToAgentEffects(agentEffectsMap, coordinates, playground);

                cells[x, y] = new MapCell(coordinates, cell.Object.Id, cell.Object.Type, agentEffects);
            }
        }

        return new MapLayoutResponse(playground.Turn, cells);
    }

    private static void AddEffectForAgent(
        Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>> agentEffectsMap,
        Coordinates coordinates,
        Guid agentId,
        EEffect effect)
    {
        if (!agentEffectsMap.ContainsKey(coordinates))
            agentEffectsMap[coordinates] = new Dictionary<Guid, HashSet<EEffect>>();

        if (!agentEffectsMap[coordinates].ContainsKey(agentId))
            agentEffectsMap[coordinates][agentId] = new HashSet<EEffect>();

        agentEffectsMap[coordinates][agentId].Add(effect);
    }

    private static AgentEffect[] ConvertToAgentEffects(
        Dictionary<Coordinates, Dictionary<Guid, HashSet<EEffect>>> agentEffectsMap,
        Coordinates coordinates,
        StandardPlayground playground)
    {
        if (!agentEffectsMap.ContainsKey(coordinates))
            return Array.Empty<AgentEffect>();

        var agentEffectsList = new List<AgentEffect>();

        foreach (var (agentId, effects) in agentEffectsMap[coordinates])
        {
            // Determine agent type based on ID
            EObjectType agentType = DetermineAgentType(agentId, playground);

            agentEffectsList.Add(new AgentEffect(agentId, agentType, effects.ToArray()));
        }

        return agentEffectsList.ToArray();
    }

    private static EObjectType DetermineAgentType(Guid agentId, StandardPlayground playground)
    {
        if (playground.Hero?.Id == agentId)
            return EObjectType.Hero;

        if (playground.Enemies.Any(e => e.Id == agentId))
            return EObjectType.Enemy;

        // Default fallback (shouldn't happen in normal cases)
        return EObjectType.Empty;
    }
}