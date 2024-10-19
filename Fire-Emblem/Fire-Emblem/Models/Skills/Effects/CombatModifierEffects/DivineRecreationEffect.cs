using Fire_Emblem.Characters;
using Fire_Emblem.Handlers;

namespace Fire_Emblem.Skills.Effects;

public class DivineRecreationEffect : IEffect
{
    private readonly AttackHandler _attackHandler = new();

    public void Apply(Character character, Character opponent)
    {
        Console.WriteLine($"el character {character.Info.Name} activa divine recreation");

        int damageReduced = SimulateCombat(opponent, character);

        if (character.IsInitiatingCombat)
            character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement += damageReduced;
        else
            character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement += damageReduced;

    }

    private int SimulateCombat(Character opponent, Character character)
    {
        int rawInflictedDamage = _attackHandler.CalculateRawInflicitedDamage(opponent, character);

        int reducedInfilictedDamage = _attackHandler.CalculateReducedDamage(rawInflictedDamage, character);
        
        int protectionAgainstAttacker = rawInflictedDamage - reducedInfilictedDamage;
        
       
        return protectionAgainstAttacker;
    }
}