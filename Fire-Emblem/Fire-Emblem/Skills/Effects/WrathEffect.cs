using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

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