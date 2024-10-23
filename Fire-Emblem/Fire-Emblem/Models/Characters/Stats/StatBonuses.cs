using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Characters.Stats;

public class StatBonuses
{
    private readonly Dictionary<StatName, int> _bonuses;

    public StatBonuses()
    {
        _bonuses = InitializeStatDictionary();
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

    public int GetBonus(StatName stat) => _bonuses[stat];
    public Dictionary<StatName, int> GetAllBonuses() => new(_bonuses);

    public void AddBonus(StatName stat, int amount)
    {
        _bonuses[stat] += amount;
    }
    public void Reset()
    {
        foreach (var key in _bonuses.Keys.ToList())
        {
            _bonuses[key] = 0;
        }
    }
}
