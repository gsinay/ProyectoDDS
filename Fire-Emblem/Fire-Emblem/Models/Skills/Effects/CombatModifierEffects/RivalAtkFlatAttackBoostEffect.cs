using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class RivalAtkFlatAttackBoostEffect : IEffect
{
    private readonly double _percentOfHp;

    public RivalAtkFlatAttackBoostEffect(double percentOfHp)
    {
        _percentOfHp = percentOfHp;
    }

    public void Apply(Character character, Character opponent)
    {
        double amountOfAtk = opponent.GeneralEffectiveAtk * _percentOfHp;
        Console.WriteLine($"el amountofAtk es {amountOfAtk}");
        Console.WriteLine($"el atk del rival es {opponent.Stats.BaseStats.GetBaseStat(StatName.Atk)} + {opponent.Stats.CombatBonuses.GetBonus(StatName.Atk)}");
        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += (int)Math.Floor(amountOfAtk);
    }
}