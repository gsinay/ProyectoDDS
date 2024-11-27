using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Characters.Stats;

public class StatPenalties
{
    private readonly Dictionary<StatName, int> _penalties;

    public StatPenalties()
    {
        _penalties = InitializeStatDictionary();
    }

    private Dictionary<StatName, int> InitializeStatDictionary()
    {
        return new Dictionary<StatName, int>
        {
            { StatName.Atk, 0 },
            { StatName.Spd, 0 },
            { StatName.Def, 0 },
            { StatName.Res, 0 }
        };
    }

    public int GetPenalty(StatName stat) => _penalties[stat];
    
    public Dictionary<StatName, int> GetAllPenalties() => new(_penalties);


    public void AddPenalty(StatName stat, int amount)
    {
        if (amount > 0)
            _penalties[stat] += amount;
    }

    public void Reset()
    {
        foreach (var key in _penalties.Keys.ToList())
        {
            _penalties[key] = 0;
        }
    }
}