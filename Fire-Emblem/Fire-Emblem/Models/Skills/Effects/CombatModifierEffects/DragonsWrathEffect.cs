using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class DragonsWrathEffect : IEffect
{

    public void Apply(Character character, Character opponent)
    {
        int extraDamage = Math.Max(0, (int)(0.25 * (character.GeneralEffectiveAtk - opponent.GeneralEffectiveRes)));
        character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement += extraDamage;
    }
}