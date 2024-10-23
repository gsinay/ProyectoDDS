using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

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

        double extraDamageDouble = Math.Round(hpLost * _lostHpPercent, 9);
        int extraDamage = (int)Math.Floor(extraDamageDouble);
        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += extraDamage;


    }
}