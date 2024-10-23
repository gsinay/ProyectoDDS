namespace Fire_Emblem.Models.Characters.Modifiers;

public class CharacterCombatModifiers
{
    public readonly Modifiers CombatModifiers = new();
    public readonly Modifiers FirstAttackModifiers = new();
    public readonly Modifiers FollowupModifiers = new();


    public void Reset()
    {
        CombatModifiers.Reset();
        FirstAttackModifiers.Reset();
        FollowupModifiers.Reset();
    }

}