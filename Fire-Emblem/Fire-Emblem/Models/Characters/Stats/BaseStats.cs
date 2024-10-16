namespace Fire_Emblem.Characters;

public class BaseStats
{
    private readonly Dictionary<StatName, int> _baseStats;

    public BaseStats(int hp, int atk, int spd, int def, int res)
    {
        _baseStats = new Dictionary<StatName, int>
        {
            { StatName.Hp, hp },
            { StatName.MaxHp, hp },
            { StatName.Atk, atk },
            { StatName.Spd, spd },
            { StatName.Def, def },
            { StatName.Res, res }
        };
    }

    public int GetBaseStat(StatName stat) => _baseStats[stat];

    public void SetBaseStat(StatName stat, int value)
    {
        _baseStats[stat] = value;
    }
}
