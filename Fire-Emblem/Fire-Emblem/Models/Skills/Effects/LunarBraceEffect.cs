using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class LunarBraceEffect : IEffect
{
    private double _statPercentThreshold;

    public LunarBraceEffect(double statPercentThreshold)
    {
        _statPercentThreshold = statPercentThreshold;
    }

    public void Apply(Character character, Character opponent)
    {
        double extraDamage = Math.Truncate(opponent.GeneralEffectiveDef * _statPercentThreshold);
        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += Convert.ToInt32(extraDamage);
    }
}