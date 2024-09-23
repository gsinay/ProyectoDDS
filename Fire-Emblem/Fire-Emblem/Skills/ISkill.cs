using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills;

public interface ISkill
{
    string Name { get; }
    string Description { get; }
    void ApplyEffect(Character? character, Character opponent);
   
}