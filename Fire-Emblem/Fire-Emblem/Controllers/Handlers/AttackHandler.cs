using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Controllers.Handlers;

public class AttackHandler
{
    private readonly WtbHandler _wtbHandler = new();  
    
    public int CalculateRawInflictedDamage(Character attacker, Character defender, string attackType)
    {
        double wtb = _wtbHandler.GetTriangleAdvantage(attacker, defender);

        int attackPower = (int)(attacker.EffectiveAtk(attackType) * wtb);
        
        int effectiveDefense = GetEffectiveDefense(attacker, defender, attackType);
        
        var atkModifierExtra = attacker.GetAttackModifier(attackType);
        

        
        int rawDamage = Math.Max(0, attackPower - effectiveDefense) + atkModifierExtra;

        return rawDamage;
    }
    
    private int GetEffectiveDefense(Character attacker, Character defender, string attackType)
    {
        if (attacker.Info.Weapon == WeaponName.Magic)
        {
            return defender.EffectiveRes(attackType);
        }
       
        return defender.EffectiveDef(attackType);
    }

    public int CalculateReducedDamage(int rawDamage, Character defender, string attackType) =>
        defender.GetAttackWithReduction(rawDamage, attackType);


    public bool CanCounterAttack(Character attacker, Character defender)
    {
       
        bool defenderCounterAttackIsNotNegated = !defender.CharacterModifiers.CombatModifiers.CounterAttackIsNegated;
        bool defenderNegatesNegation = defender.CharacterModifiers.CombatModifiers.NegatedCounterAttackNegation;
        
        bool defenderCanCounterAttack = defenderCounterAttackIsNotNegated || defenderNegatesNegation;

        return defender.IsAlive() && defenderCanCounterAttack;
    }
}