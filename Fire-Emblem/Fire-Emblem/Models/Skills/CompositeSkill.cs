using Fire_Emblem.Characters;
using Fire_Emblem.Collections;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills.Conditions;
using Fire_Emblem.SkillsManager;

namespace Fire_Emblem.Skills
{
    
    public class CompositeSkill : BaseSkill
    {
        private SkillList _skills;

        public CompositeSkill(string name, string description, SkillList skills)
            : base(name, description) 
        {
            _skills = skills;
        }
        public CompositeSkill(string name, SkillList skills)
            : base(name) 
        {
            _skills = skills;
        }
        
        public void ApplyBasicSkills(Character character, Character opponent)
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is BasicSkill basicSkill)
                    basicSkill.ApplySkill(character, opponent);
            }
        }
        
        public void ApplySecondDegreeSkills(Character character, Character opponent)
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is SecondDegreeSkill secondDegreeSkill)
                    secondDegreeSkill.ApplySkill(character, opponent);
            }
        }
        public void ApplyThirdDegreeSkills(Character character, Character opponent)
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is ThirdDegreeSkill thirdDegreeSkill)
                    thirdDegreeSkill.ApplySkill(character, opponent);
            }
        }
    }
}