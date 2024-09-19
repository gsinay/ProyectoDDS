using Fire_Emblem.Characters;
using Fire_Emblem.Combat;

namespace Fire_Emblem.Skills;

public interface ISkill
{
    string Name { get; }
    string Description { get; }
    void ApplyEffect(Character character, Character opponent, CombatLog combatLog);
   
}