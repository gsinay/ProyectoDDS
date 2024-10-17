using Fire_Emblem.Characters;
using Fire_Emblem.Handlers;

namespace Fire_Emblem.Skills.Effects;

public class DivineRecreationEffect : IEffect
{
    private readonly AttackHandler _attackHandler = new();

    public void Apply(Character character, Character opponent)
    {
        SimulateCombat(character, opponent);
        if (character.IsInitiatingCombat)
        {
            Console.WriteLine($"el character {character.Info.Name} activa divine recreation");
            int rawInflictedDamage = _attackHandler.GetRawInflicitedDamage(opponent, character);
            Console.WriteLine($"el raw inflicted damge es de {rawInflictedDamage}");
            int inflictedDamage = _attackHandler.CalculateReducedDamage(rawInflictedDamage, character);
            Console.WriteLine($"el actual inflicted damage es de {inflictedDamage}");
            int amountOfDamageReduced = rawInflictedDamage - inflictedDamage;
            character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement += amountOfDamageReduced;
        }
        else
        {
            Console.WriteLine($"el character {character.Info.Name} activa divine recreation");
            int rawInflictedDamage = _attackHandler.GetRawInflicitedDamage(opponent, character);
            Console.WriteLine($"el raw inflicted damge es de {rawInflictedDamage}");
            int inflictedDamage = _attackHandler.CalculateReducedDamage(rawInflictedDamage, character);
            Console.WriteLine($"el actual inflicted damage es de {inflictedDamage}");
            int amountOfDamageReduced = rawInflictedDamage - inflictedDamage;
            character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement += amountOfDamageReduced;
        }
    }

    private void SimulateCombat(Character character, Character opponent)
    {
        int rawdamageattacker = _attackHandler.GetRawInflicitedDamage(character, opponent);
        Console.WriteLine($"the inflicted raw damage is {rawdamageattacker} ");
        int damageattacker = _attackHandler.CalculateReducedDamage(rawdamageattacker, opponent);
        Console.WriteLine($"the actual  damage is {damageattacker} ");
        //esto es lo extra que hará Celica
        int protectionattacker = rawdamageattacker - damageattacker;
        Console.WriteLine($"the protecion attacker is {protectionattacker}");
        
        //calcular ahora para el defender
        int rawdamageadefender = _attackHandler.GetRawInflicitedDamage(opponent, character);
        //sumar la proteccion
        int damagedefender = _attackHandler.CalculateReducedDamage(rawdamageadefender, character);
        int protectiondefender = damagedefender - rawdamageadefender;

    }
}