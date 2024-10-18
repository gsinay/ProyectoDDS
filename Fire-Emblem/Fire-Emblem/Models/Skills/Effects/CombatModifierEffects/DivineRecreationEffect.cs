using Fire_Emblem.Characters;
using Fire_Emblem.Handlers;

namespace Fire_Emblem.Skills.Effects;

public class DivineRecreationEffect : IEffect
{
    private readonly AttackHandler _attackHandler = new();

    public void Apply(Character character, Character opponent)
    {
        Console.WriteLine($"el character {character.Info.Name} activa divine recreation");

        int damagereduced = SimulateCombat(opponent, character);

        if (character.IsInitiatingCombat)
            character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement += damagereduced;
        else
            character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement += damagereduced;

    }

    private int SimulateCombat(Character opponent, Character character)
    {
        int rawdamageattacker = _attackHandler.GetRawInflicitedDamage(opponent, character);

        int damageattacker = _attackHandler.CalculateReducedDamage(rawdamageattacker, character);

        //cuanto yo me defiendo
        int protectionattacker = rawdamageattacker - damageattacker;
        
        /*Console.WriteLine($"the opponent got protected and made {protectionattacker} less damage");
        
        int rawdamageadefender = _attackHandler.GetRawInflicitedDamage(opponent, character);

        rawdamageadefender += protectionattacker;

        int damagedefender = _attackHandler.CalculateReducedDamage(rawdamageadefender, character);

        int protectiondefender = rawdamageadefender - damagedefender;

        if (isInitiatingCombat)
            return protectiondefender;*/
        return protectionattacker;
    }
}