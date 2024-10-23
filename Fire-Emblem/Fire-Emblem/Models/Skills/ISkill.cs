using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills;

public interface ISkill
{
    string Name { get; }
    string Description { get; }
    void ApplySkill(Character character, Character opponent);
   
}