using Fire_Emblem.Characters;

namespace Fire_Emblem.Handlers;

public class AttackHandler
{
    private readonly WtbHandler _wtbHandler = new();  
    
    public int CalculateRawInflicitedDamage(Character attacker, Character defender)
    {
        double wtb = _wtbHandler.GetTriangleAdvantage(attacker, defender);

        int attackPower = (int)(attacker.EffectiveAtk * wtb);

        
        int effectiveDefense = GetEffectiveDefense(attacker, defender);

        
        var atkModifierExtra = attacker.GetAttackModifier();
        

        
        int rawDamage = Math.Max(0, attackPower - effectiveDefense) + atkModifierExtra;

        return rawDamage;
    }
    
    private int GetEffectiveDefense(Character attacker, Character defender)
    {
        if (attacker.Info.Weapon == WeaponName.Magic)
        {
            return defender.EffectiveRes;
        }
       
        return defender.EffectiveDef;
    }

    public int CalculateReducedDamage(int rawDamage, Character defender) => defender.GetAttackWithReduction(rawDamage);
    
    
}