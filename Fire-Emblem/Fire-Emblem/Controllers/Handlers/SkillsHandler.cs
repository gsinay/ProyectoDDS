using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Skills;

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
        var combatContext = (character, opponent); 
        foreach (var skill in character.Skills)
        {
            ApplySkillBasedOnDegree(skill, combatContext, skillDegree);
        }
    }

    private void ApplySkillBasedOnDegree(ISkill skill, (Character character, Character opponent) combatContext, 
        int skillDegree)
    {
        if (skill is CompositeSkill compositeSkill)
        {
            ApplyCompositeSkillByDegree(compositeSkill, combatContext, skillDegree);
        }
        else
        {
            ApplyNonCompositeSkillByDegree(skill, combatContext, skillDegree);
        }
    }

    private void ApplyCompositeSkillByDegree(CompositeSkill compositeSkill, 
        (Character character, Character opponent) combatContext, int skillDegree)
    {
        if (skillDegree == 1)
        {
            compositeSkill.ApplyBasicSkills(combatContext.character, combatContext.opponent);
        }
        else if (skillDegree == 2)
        {
            compositeSkill.ApplySecondDegreeSkills(combatContext.character, combatContext.opponent);
        }
        else if (skillDegree == 3)
        {
            compositeSkill.ApplyThirdDegreeSkills(combatContext.character, combatContext.opponent);
        }
    }

    private void ApplyNonCompositeSkillByDegree(ISkill skill, (Character character, Character opponent) combatContext, int skillDegree)
    {
        if (skillDegree == 1 && skill is BasicSkill basicSkill)
        {
            basicSkill.ApplySkill(combatContext.character, combatContext.opponent);
        }
        else if (skillDegree == 2 && skill is SecondDegreeSkill secondDegreeSkill)
        {
            secondDegreeSkill.ApplySkill(combatContext.character, combatContext.opponent);
        }
        else if (skillDegree == 3 && skill is ThirdDegreeSkill thirdDegreeSkill)
        {
            thirdDegreeSkill.ApplySkill(combatContext.character, combatContext.opponent);
        }
    }
}
