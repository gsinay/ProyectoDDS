using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills;

namespace Fire_Emblem.Controllers.Handlers;

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
    
    public void ApplySkillsBeforeCombat(Character character, Character opponent, int skillDegree)
    {
        foreach (var skill in character.Skills)
        {
            if (skill is CompositeSkill compositeSkill)
            {
                ApplyCompositeSkill(compositeSkill, character, opponent, skillDegree);
                continue;
            }

            ApplyNonCompositeSkill(skill, character, opponent, skillDegree);
        }
    }

    private void ApplyCompositeSkill(CompositeSkill compositeSkill, Character character, Character opponent, int skillDegree)
    {
        if (skillDegree == 1)
        {
            compositeSkill.ApplyBasicSkills(character, opponent);
        }
        else if (skillDegree == 2)
        {
            compositeSkill.ApplySecondDegreeSkills(character, opponent);
        }
        else 
        {
            compositeSkill.ApplyThirdDegreeSkills(character, opponent);
        }
    }

    private void ApplyNonCompositeSkill(ISkill skill, Character character, Character opponent, int skillDegree)
    {
        if (skillDegree == 1 && skill is BasicSkill basicSkill)
        {
            basicSkill.ApplySkill(character, opponent);
        }
        else if (skillDegree == 2 && skill is SecondDegreeSkill secondDegreeSkill)
        {
            secondDegreeSkill.ApplySkill(character, opponent);
        }
        else if (skillDegree == 3 && skill is ThirdDegreeSkill thirdDegreeSkill)
        {
            thirdDegreeSkill.ApplySkill(character, opponent);
        }
    }


}