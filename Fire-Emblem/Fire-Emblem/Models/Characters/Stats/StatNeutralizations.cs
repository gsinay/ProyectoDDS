using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Characters.Stats;

public class StatNeutralizations
{
    private readonly Dictionary<StatName, bool> _neutralizations;

    public StatNeutralizations()
    {
        _neutralizations = InitializeNeutralizationDictionary();
    }

    private Dictionary<StatName, bool> InitializeNeutralizationDictionary()
    {
        return new Dictionary<StatName, bool>
        {
            { StatName.Atk, false },
            { StatName.Spd, false },
            { StatName.Def, false },
            { StatName.Res, false }
        };
    }

    public bool IsNeutralized(StatName stat) => _neutralizations[stat];
    
    public Dictionary<StatName, bool> GetAllNeutralizations() => new(_neutralizations);


    public void Neutralize(StatName stat)
    {
        _neutralizations[stat] = true;
    }

    public void Reset()
    {
        foreach (var key in _neutralizations.Keys.ToList())
        {
            _neutralizations[key] = false;
        }
    }
}
