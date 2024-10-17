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
        
        public void ApplyBasicSkills(Character character, Character opponent)
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is BasicSkill basicSkill)
                    basicSkill.ApplySkill(character, opponent);
            }
        }
        
        public void ApplyModifierSkills(Character character, Character opponent)
        {
            foreach (var skill in _skills.GetSkills())
            {
                if (skill is ModifierSkill modifierSkill)
                    modifierSkill.ApplySkill(character, opponent);
            }
        }
    }
}