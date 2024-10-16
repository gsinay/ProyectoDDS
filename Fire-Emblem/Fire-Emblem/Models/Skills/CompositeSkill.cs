using Fire_Emblem.Characters;
using Fire_Emblem.Collections;
using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Skills
{
    
    public class CompositeSkill : ModifierSkill
    {
        private ConditionalEffectsList _conditionalEffects;

        public CompositeSkill(string name, string description, ConditionalEffectsList conditionalEffects)
            : base(name, description) 
        {
            _conditionalEffects = conditionalEffects;
        }

       
        public override void ApplySkill(Character character, Character opponent)
        {
            foreach (var conditionalEffect in _conditionalEffects.GetConditionalEffects())
            {
                conditionalEffect.ApplyEffects(character, opponent);
            }
        }
    }
}