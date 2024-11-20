using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class HealingAfterAttackEffect :IEffect
{
    private double _percent;

    public HealingAfterAttackEffect(double percent)
    {
        _percent = percent;
    }

    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.PercentHealingReceivedAfterAttack += _percent;
    }
    
}