using Fire_Emblem;
using Fire_Emblem.Characters;

public class WrathEffect : IEffect
{
    private const int MaxBonus = 30;

    public void Apply(Character character, Character opponent)
    {
        int hpLost = character.GetMaxHP - character.GetHP; 
        int bonus = Math.Min(hpLost, MaxBonus); 

        character.Stats.CombatBonuses["Atk"] += bonus;
        character.Stats.CombatBonuses["Spd"] += bonus;
    }
    
}