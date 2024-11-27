namespace Fire_Emblem.Models.Characters.Calculators;

public class DamageCalculator
{  
    public int GetAttackWithReduction(Character character, int originalAttack, string attackType)
        {
            int attackAfterPercentageReduction = ApplyPercentageReduction(character, originalAttack, attackType);
            int totalAbsoluteReduction = CalculateTotalAbsoluteReduction(character, attackType);
            int finalAttack = attackAfterPercentageReduction - totalAbsoluteReduction;
            return Math.Max(0, finalAttack);
        }

        private int ApplyPercentageReduction(Character character, int originalAttack, string attackType)
        {
            double cumulativePercentDamageReceived = CalculateCumulativePercentDamageReceived(character, attackType);
            double reducedAttack = Math.Round(originalAttack * cumulativePercentDamageReceived, 9);
            return (int)Math.Floor(reducedAttack);
        }
        
        private double CalculateCumulativePercentDamageReceived(Character character, string attackType)
        {
            
            double percentDamageReceived = character.CharacterModifiers.CombatModifiers.PercentDamageReceived;
            if (attackType == "first")
                percentDamageReceived *= character.CharacterModifiers.FirstAttackModifiers.PercentDamageReceived;
            else
                percentDamageReceived *= character.CharacterModifiers.FollowupModifiers.PercentDamageReceived;
            return percentDamageReceived;
        }

        private int CalculateTotalAbsoluteReduction(Character character, string attackType)
        {
            int absoluteReduction = character.CharacterModifiers.CombatModifiers.FlatDamageReduction;
            int additionalReduction = attackType == "followup"
                ? character.CharacterModifiers.FollowupModifiers.FlatDamageReduction
                : character.CharacterModifiers.FirstAttackModifiers.FlatDamageReduction;
            return absoluteReduction + additionalReduction;
        }

    
        public  int GetAttackModifier(Character character, string attackType)
        {
            int extraAttack = character.CharacterModifiers.CombatModifiers.FlatAttackIncrement;
            if (attackType == "first")
                extraAttack += character.CharacterModifiers.FirstAttackModifiers.FlatAttackIncrement;
            else 
                extraAttack += character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement;
            return extraAttack;
        }
    
}