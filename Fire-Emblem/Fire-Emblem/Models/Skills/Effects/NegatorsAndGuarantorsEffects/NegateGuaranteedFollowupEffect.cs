using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.NegatorsAndGuarantorsEffects;

public class NegateGuaranteedFollowupEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        opponent.CharacterModifiers.CombatModifiers.NegatedGuaranteedFollowup = true;
    }
    
}