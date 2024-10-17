using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class LostHpExtraFlatDamageEffect : IEffect
{
    private readonly double _lostHpPercent;

    public LostHpExtraFlatDamageEffect(double lostHpPercent)
    {
        _lostHpPercent = lostHpPercent;
    }

    public void Apply(Character character, Character opponent)
    {
        int hpLost = character.Stats.BaseStats.GetBaseStat(StatName.MaxHp) -
                     character.Stats.BaseStats.GetBaseStat(StatName.Hp);

        int extraDamage = Convert.ToInt32(hpLost * _lostHpPercent);
        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += extraDamage;


    }
}