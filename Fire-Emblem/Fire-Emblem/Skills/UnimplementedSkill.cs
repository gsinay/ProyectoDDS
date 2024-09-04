using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills;

public class UnimplementedSkill : ISkill
{
    
    public string Name { get; }
    public string Description { get; }
    public UnimplementedSkill(string skillName)
    {
        Name = skillName;
        Description = $"The skill '{skillName}' has not been implemented yet.";
    }

    public void ApplyEffect(Character character, Character opponent, CombatLog combatLog)
    {
    }

    public void RemoveEffect(Character character, Character opponent)
    {
      
    }
}