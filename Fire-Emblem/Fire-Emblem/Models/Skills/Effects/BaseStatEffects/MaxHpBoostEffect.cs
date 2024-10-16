using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class MaxHpBoostEffect : IEffect
{
    private readonly int _hpBoostAmount;
    
    public MaxHpBoostEffect(int hpBoostAmount)
    {
        _hpBoostAmount = hpBoostAmount;
    }

    public void Apply(Character character, Character opponent)
    {
        character.IncreaseMaxHp(_hpBoostAmount);
    }
}