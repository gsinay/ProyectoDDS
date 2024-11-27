using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.NegatorsAndGuarantorsEffects;

public class NegateFollowupSelfEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.NegatedFollowupCounter += 1;
    }
}