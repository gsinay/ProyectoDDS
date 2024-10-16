using Fire_Emblem.Characters;
using Fire_Emblem.Collections;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Models.Skills;

public class ConditionalEffect {
    private readonly ICondition _condition;  
    private readonly EffectsList _effects; 

    public ConditionalEffect(ICondition condition, EffectsList effects) {
        _condition = condition;
        _effects = effects;
    }
    public void ApplyEffects(Character character, Character rival) {
        if (_condition.IsSatisfied(character, rival)) {
            foreach (var effect in _effects.GetEffects())
            {
                effect.Apply(character, rival);
            }
        }
    }
}
