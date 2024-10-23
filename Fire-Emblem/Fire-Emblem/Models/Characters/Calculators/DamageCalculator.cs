namespace Fire_Emblem.Models.Characters.Calculators;

public class DamageCalculator
{  
    public int GetAttackWithReduction(Character character, int originalAttack)
        {
            int attackAfterPercentageReduction = ApplyPercentageReduction(character, originalAttack);
            int totalAbsoluteReduction = CalculateTotalAbsoluteReduction(character);
            int finalAttack = attackAfterPercentageReduction - totalAbsoluteReduction;
            return Math.Max(0, finalAttack);
        }

        private int ApplyPercentageReduction(Character character, int originalAttack)
        {
            double cumulativePercentDamageReceived = CalculateCumulativePercentDamageReceived(character);
            double reducedAttack = Math.Round(originalAttack * cumulativePercentDamageReceived, 9);
            return (int)Math.Floor(reducedAttack);
        }
        
        private double CalculateCumulativePercentDamageReceived(Character character)
        {
            
            double percentDamageReceived = character.CharacterModifiers.CombatModifiers.PercentDamageReceived;
            if (!character.HasAttacked)
                percentDamageReceived *= character.CharacterModifiers.FirstAttackModifiers.PercentDamageReceived;
            else
                percentDamageReceived *= character.CharacterModifiers.FollowupModifiers.PercentDamageReceived;
            return percentDamageReceived;
        }

        private int CalculateTotalAbsoluteReduction(Character character)
        {
            int absoluteReduction = character.CharacterModifiers.CombatModifiers.FlatDamageReduction;
            int additionalReduction = character.HasAttacked 
                ? character.CharacterModifiers.FollowupModifiers.FlatDamageReduction
                : character.CharacterModifiers.FirstAttackModifiers.FlatDamageReduction;
            return absoluteReduction + additionalReduction;
        }

    
        public  int GetAttackModifier(Character character)
        {
            int extraAttack = character.CharacterModifiers.CombatModifiers.FlatAttackIncrement;
            if (!character.HasAttacked)
                extraAttack += character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement;
            else 
                extraAttack += character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement;
            return extraAttack;
        }
    
}