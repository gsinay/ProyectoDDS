using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class DragonsWrathEffect : IEffect
{

    public void Apply(Character character, Character opponent)
    {
        Console.WriteLine($"el atk de la unidad es {character.GeneralEffectiveAtk} y el res del oponente es {opponent.GeneralEffectiveRes} ");
        int extraDamage = Math.Max(0, (int)(0.25 * (character.GeneralEffectiveAtk - opponent.GeneralEffectiveRes)));
        Console.WriteLine($"el damage extra realizado es {extraDamage}");
        character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement += extraDamage;
    }
}