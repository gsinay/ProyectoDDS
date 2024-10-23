using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.BaseStatEffects;

public class WrathEffect : IEffect
{
    private const int MaxBonus = 30;

    public void Apply(Character character, Character opponent)
    {
        int bonus = CalculateBonus(character);
        ApplyBonusToStats(character, bonus);
    }

    private int CalculateBonus(Character character)
    {
        int hpLost = character.GetMaxHp - character.GetHp;
        return Math.Min(hpLost, MaxBonus);
    }

    private void ApplyBonusToStats(Character character, int bonus)
    {
        character.Stats.CombatBonuses.AddBonus(StatName.Atk, bonus);
        character.Stats.CombatBonuses.AddBonus(StatName.Spd, bonus);
    }
}