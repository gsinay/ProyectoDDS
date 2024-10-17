using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class GuardBearingEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        Console.WriteLine($"el character {character.Info.Name} ha iniciado combate: {character.HasInitiatedCombat}");
        Console.WriteLine($"el character {character.Info.Name} ha defendido: {character.HasDefendedCombat}");
        Console.WriteLine($"el character {opponent.Info.Name} ha iniciado combate: {opponent.HasInitiatedCombat}");
        Console.WriteLine($"el character {opponent.Info.Name} ha defendido: {opponent.HasDefendedCombat}");



        if(character is { IsInitiatingCombat: true, HasInitiatedCombat: false })
            character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(0.6);
        else if (character is { IsInitiatingCombat: false, HasDefendedCombat: false })
            character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(0.6);
        else 
            character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(0.3);

        
    }
}