using Fire_Emblem.Characters;
using Fire_Emblem.Skills;

namespace Fire_Emblem.Handlers;

public class SkillsHandler
{
    public void ApplyPermanentEffects(Character character)
    {
        foreach (var skill in character.Skills)
        {
            if (skill is OneTimeSkill permanentSkill)
            {
                permanentSkill.ApplySkill(character, null!);
            }
        }
    }

    public void ApplyBasicSkillsBeforeCombat(Character character, Character opponent)
    {
        foreach (var skill in character.Skills)
        {
            if (skill is BasicSkill basicSkill)
                basicSkill.ApplySkill(character, opponent);
            if (skill is CompositeSkill compositeSkill)
                compositeSkill.ApplyBasicSkills(character, opponent);
        }
    }

    public void ApplySecondDegreeSkillsBeforeCombat(Character character, Character opponent)
    {
        foreach (var skill in character.Skills)
        {
            if (skill is SecondDegreeSkill secondDegreeSkill)
                secondDegreeSkill.ApplySkill(character, opponent);
            if (skill is CompositeSkill compositeSkill)
                compositeSkill.ApplySecondDegreeSkills(character, opponent);
                
        }
    }
    public void ApplyThirdDegreeSkillsBeforeCombat(Character character, Character opponent)
    {
        foreach (var skill in character.Skills)
        {
            if (skill is ThirdDegreeSkill thirdDegreeSkill)
                thirdDegreeSkill.ApplySkill(character, opponent);
            if (skill is CompositeSkill compositeSkill)
                compositeSkill.ApplyThirdDegreeSkills(character, opponent);
                
        }
    }
}