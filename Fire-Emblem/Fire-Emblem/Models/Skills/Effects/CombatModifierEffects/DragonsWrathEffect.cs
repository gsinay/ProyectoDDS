using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class DragonsWrathEffect : IEffect
{

    public void Apply(Character character, Character opponent)
    {
        int extraDamage = Math.Max(0, (int)(0.25 * (character.GeneralEffectiveAtk - opponent.GeneralEffectiveRes)));
        character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement += extraDamage;
    }
}