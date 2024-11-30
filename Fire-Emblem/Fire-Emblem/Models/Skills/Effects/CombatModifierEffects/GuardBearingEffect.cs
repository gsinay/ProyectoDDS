using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class GuardBearingEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        double reduction = GetDamageReduction(character);
        character.CharacterModifiers.CombatModifiers.DamageReduction.AddEffect(reduction);
    }
    private double GetDamageReduction(Character character)
    {
        if (character is { IsInitiatingCombat: true, HasInitiatedCombat: false })
            return 0.6;
        if (character is { IsInitiatingCombat: false, HasDefendedCombat: false })
            return 0.6;
        return 0.3;
    }
}
