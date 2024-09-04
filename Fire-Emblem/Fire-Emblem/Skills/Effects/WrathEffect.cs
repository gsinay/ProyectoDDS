using Fire_Emblem;
using Fire_Emblem.Characters;

public class WrathEffect : IEffect
{
    private const int MaxBonus = 30;

    public void Apply(Character character, Character opponent, CombatLog combatLog)
    {
        int hpLost = character.GetMaxHP - character.GetHP; 
        int bonus = Math.Min(hpLost, MaxBonus); 

        character.AtkModifier += bonus;
        character.SpdModifier += bonus;

        combatLog.LogBonus(character, "Atk", bonus);
        combatLog.LogBonus(character, "Spd", bonus);
    }
    
}