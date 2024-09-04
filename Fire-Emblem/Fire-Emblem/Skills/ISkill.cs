using Fire_Emblem;
using Fire_Emblem.Characters;

public interface ISkill
{
    string Name { get; }
    string Description { get; }
    void ApplyEffect(Character character, Character opponent, CombatLog combatLog);
   
}