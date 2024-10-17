using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class ExtraChivalryEffect : IEffect
{
    private readonly double _percentMultiplier;

    public ExtraChivalryEffect(double percentMultiplier)
    {
        _percentMultiplier = percentMultiplier;
    }
    public void Apply(Character character, Character opponent)
    {
        double percentOfRemainingHp = opponent.GetRemainingHpPercentage() * _percentMultiplier;
        double truncatedValue = Math.Truncate(percentOfRemainingHp * 100) / 100;
        character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(truncatedValue);
    }
}