namespace Fire_Emblem.Models.Characters.Modifiers;

public class Modifiers
{
    public int FlatAttackIncrement;
    public int FlatDamageReduction;
    public double PercentDamageReceived = 1.0
        ;

    public void ReducePercentageOfDamageReceived(double reductionFactor)
    {
        PercentDamageReceived *= (1 - reductionFactor);
    }

    public void Reset()
    {
        FlatAttackIncrement = 0;
        FlatDamageReduction = 0;
        PercentDamageReceived = 1;
    }
}