namespace Fire_Emblem.Models.Characters.Modifiers;

public class Modifiers
{
    public int FlatAttackIncrement;
    public int FlatDamageReduction;
    public double PercentDamageReceived = 1.0;
    public double PercentHealingReceivedAfterAttack = 0;
    public bool CounterAttackIsNegated = false;
    public bool NegatedCounterAttackNegation = false;
    public int BeforeCombatHpReduction = 0;
    public int AfterCombatHpChange = 0;
   


    public void ReducePercentageOfDamageReceived(double reductionFactor)
    {
        PercentDamageReceived *= (1 - reductionFactor);
    }

    public void Reset()
    {
        FlatAttackIncrement = 0;
        FlatDamageReduction = 0;
        PercentDamageReceived = 1;
        PercentHealingReceivedAfterAttack = 0;
        CounterAttackIsNegated = false;
        NegatedCounterAttackNegation = false;
        BeforeCombatHpReduction = 0;
        AfterCombatHpChange = 0;
    }
}