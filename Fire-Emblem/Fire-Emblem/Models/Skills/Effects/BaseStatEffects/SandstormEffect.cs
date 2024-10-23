using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class SandstormEffect : IEffect
{
    private const double DefenseMultiplier = 1.5;

    public void Apply(Character character, Character opponent)
    {
        int truncatedZ = CalculateTruncatedZ(character);
        ApplyAttackDifference(character, truncatedZ);
    }

    private int CalculateTruncatedZ(Character character)
    {
        double z = Math.Floor(DefenseMultiplier * character.Stats.BaseStats.GetBaseStat(StatName.Def));
        return Convert.ToInt32(z);
    }

    private void ApplyAttackDifference(Character character, int truncatedZ)
    {
        int atkDifference = truncatedZ - character.Stats.BaseStats.GetBaseStat(StatName.Atk);
        if (atkDifference > 0)
        {
            character.Stats.FollowupBonuses.AddBonus(StatName.Atk, atkDifference);
        }
        else
        {
            int penalty = Math.Abs(atkDifference);
            character.Stats.FollowupPenalties.AddPenalty(StatName.Atk, penalty);
        }
    }
}