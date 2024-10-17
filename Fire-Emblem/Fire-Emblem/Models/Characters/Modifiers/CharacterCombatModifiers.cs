namespace Fire_Emblem.Models.Characters.Modifiers;

public class CharacterCombatModifiers
{
    public Modifiers CombatModifiers = new();
    public Modifiers FirstAttackModifiers = new();
    public Modifiers FollowupModifiers = new();


    public void Reset()
    {
        CombatModifiers.Reset();
        FirstAttackModifiers.Reset();
        FollowupModifiers.Reset();
    }

}