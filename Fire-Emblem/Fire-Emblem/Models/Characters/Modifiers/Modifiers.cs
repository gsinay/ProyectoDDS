namespace Fire_Emblem.Models.Characters.Modifiers;

public class Modifiers
{
    public int FlatAttackIncrement;
    public int FlatDamageReduction;
    public double PercentDamageReceived = 1.0;

    public bool CounterAttackIsNegated = false;
    public bool NegatedCounterAttackNegation = false;
    public bool NegatedGuaranteedFollowup = false;
    public bool NegateNegatedFollowup = false;
    
    public double PercentHealingReceivedAfterAttack = 0;
    public int BeforeCombatHpReduction = 0;
    public int AfterCombatHpChange = 0;
    public int AfterCombatIfAttackedHpChange = 0;
    public int GuaranteedFollowupCounter = 0;
    public int NegatedFollowupCounter = 0;
   
   


    public void ReducePercentageOfDamageReceived(double reductionFactor)
    {
        PercentDamageReceived *= (1 - reductionFactor);
    }

    public void Reset()
    {
        FlatAttackIncrement = 0;
        FlatDamageReduction = 0;
        PercentDamageReceived = 1;
        
       
        CounterAttackIsNegated = false;
        NegatedCounterAttackNegation = false;
        NegatedGuaranteedFollowup = false;
        NegateNegatedFollowup = false;

        BeforeCombatHpReduction = 0;
        AfterCombatHpChange = 0;
        AfterCombatIfAttackedHpChange = 0;
        GuaranteedFollowupCounter = 0;
        NegatedFollowupCounter = 0;
        PercentHealingReceivedAfterAttack = 0;
        
       
    }
}