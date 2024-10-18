using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class GuardBearingEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
       
        if(character is { IsInitiatingCombat: true, HasInitiatedCombat: false })
            character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(0.6);
        else if (character is { IsInitiatingCombat: false, HasDefendedCombat: false })
            character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(0.6);
        else 
            character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(0.3);

        
    }
}