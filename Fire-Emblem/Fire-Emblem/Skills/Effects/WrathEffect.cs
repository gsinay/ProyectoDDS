using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class WrathEffect : IEffect
{
    private const int MaxBonus = 30;

    public void Apply(Character? character, Character opponent)
    {
        int hpLost = character.GetMaxHp - character.GetHp; 
        int bonus = Math.Min(hpLost, MaxBonus); 

        character.Stats.CombatBonuses[StatName.Atk] += bonus;
        character.Stats.CombatBonuses[StatName.Spd] += bonus;
    }
    
}