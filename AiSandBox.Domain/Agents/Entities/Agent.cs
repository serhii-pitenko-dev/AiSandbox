using AiSandBox.Domain.Maps;
using AiSandBox.SharedBaseTypes.ValueObjects;
using System.Text.Json.Serialization;

namespace AiSandBox.Domain.Agents.Entities;

public abstract class Agent: SandboxMapBaseObject
{
    // Parameterless constructor for deserialization
    protected Agent() : base()
    {
    }

    [JsonInclude]
    public List<Cell> VisibleCells { get; protected set; } = new();
    public Agent(
        EObjectType cellType,
        InitialAgentCharacters characters,
        Cell cell, 
        Guid id) : base(cellType, cell, id)
    {
        Speed = characters.Speed;
        SightRange = characters.SightRange;
        MaxStamina = Stamina = characters.Stamina;
    }

    [JsonInclude]
    public List<Coordinates> PathToTarget { get; protected set; } = [];

    [JsonInclude]
    public int Speed { get; protected set; }

    [JsonInclude]
    public int SightRange { get; protected set; }

    [JsonInclude]
    public bool IsRun { get; protected set; } = false;

    [JsonInclude]
    public int Stamina { get; protected set; }

    [JsonInclude]
    public int MaxStamina { get; protected set; }

    [JsonInclude]
    public int OrderInTurnQueue { get; set; } = 0;

    protected void CopyBaseTo(Agent target)
    {
        base.CopyTo(target);
        target.Speed = Speed;
        target.SightRange = SightRange;
        target.IsRun = IsRun;
        target.Stamina = Stamina;
        target.MaxStamina = MaxStamina;
        target.PathToTarget = [.. PathToTarget];
        target.VisibleCells = [.. VisibleCells];
        target.Transparent = Transparent;
    }

    public void ResetPath()
    {
        PathToTarget.Clear();
    }

    public void AddToPath(List<Coordinates> coordinates)
    {
        PathToTarget.AddRange(coordinates);
    }

    public virtual List<EAction> ActivateAbilities(EAction[] abilities)
    {
        var activatedAbilities = new List<EAction>();

        foreach (EAction ability in abilities)
        {
            switch (ability)
            {
                case EAction.Run:
                    if (IsRun)
                        break;
                    IsRun = true;
                    Speed += 1;
                    activatedAbilities.Add(ability);

                    break;
            }
        }

        return activatedAbilities;
    }

    public virtual List<EAction> DeActivateAbility(EAction[] abilities)
    {
        var deactivatedAbilities = new List<EAction>();

        foreach (EAction ability in abilities)
        {
            switch (ability)
            {
                case EAction.Run:
                    if (!IsRun)
                        break;

                    IsRun = false;
                    Speed -= 1;
                    deactivatedAbilities.Add(ability);

                    break;
            }
        }

        return deactivatedAbilities;    
    }

    public virtual void GetReadyForNewTurn()
    {
        ResetPath();

        //add current/started position to path
        AddToPath(new List<Coordinates>() { Coordinates });
    }

    public void SetOrderInTurnQueue(int order)
    {
        OrderInTurnQueue = order;
    }
}