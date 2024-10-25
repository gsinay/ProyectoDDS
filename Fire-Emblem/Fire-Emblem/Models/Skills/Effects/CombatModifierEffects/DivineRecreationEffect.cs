using Fire_Emblem.Controllers.Handlers;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class DivineRecreationEffect : IEffect
{
    private readonly AttackHandler _attackHandler = new();

    public void Apply(Character character, Character opponent)
    {
        int damageReduced = SimulateCombat(opponent, character);

        if (character.IsInitiatingCombat)
            character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement += damageReduced;
        else
            character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement += damageReduced;

    }

    private int SimulateCombat(Character opponent, Character character)
    {
        


        int rawInflictedDamage = _attackHandler.CalculateRawInflictedDamage(opponent, character);
        
        int reducedInfilictedDamage = _attackHandler.CalculateReducedDamage(rawInflictedDamage, character);
        
        int protectionAgainstAttacker = rawInflictedDamage - reducedInfilictedDamage;
        
       
        return protectionAgainstAttacker;
    }
}