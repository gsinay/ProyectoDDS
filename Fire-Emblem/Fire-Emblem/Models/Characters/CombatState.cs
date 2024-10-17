namespace Fire_Emblem.Characters;

public class CombatState
{
    public bool IsInitiatingCombat { get; set; }
    public Character? MostRecentOpponent { get; private set; }
    public bool HasAttacked { get; private set; }
    
    public void UpdateMostRecentOpponent(Character opponent)
    {
        MostRecentOpponent = opponent;
    }

    public void MarkAsAttacked()
    {
        HasAttacked = true;
    }

    public void MarkAsNotAttacked()
    {
        HasAttacked = false;
    }
    
}