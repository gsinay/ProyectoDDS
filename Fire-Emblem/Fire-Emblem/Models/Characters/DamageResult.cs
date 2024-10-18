namespace Fire_Emblem.Models.Characters;

public class DamageResult
{
    public int FinalDamage { get; }
    public int DamageReduced { get; }

    public DamageResult(int finalDamage, int damageReduced)
    {
        FinalDamage = finalDamage;
        DamageReduced = damageReduced;
    }
}