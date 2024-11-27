using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.NegatorsAndGuarantorsEffects;

public class NegateFollowupRivalEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        opponent.CharacterModifiers.CombatModifiers.NegatedFollowupCounter += 1;
    }
}