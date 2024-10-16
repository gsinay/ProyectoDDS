using Fire_Emblem.Characters;
using Fire_Emblem.Collections;
using Fire_Emblem.Skills.Conditions;
using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.Skills;

public abstract class BaseSkill : ISkill
{
    public string Name { get; }
    public string Description { get; }
    protected readonly ICondition _condition;  
    protected readonly EffectsList _effects;

    protected BaseSkill(string name, string description, ICondition condition, EffectsList effects)
    {
        Name = name;
        Description = description;
        _condition = condition;  
        _effects = effects;
    }

    public void ApplySkill(Character character, Character opponent)
    {
        if (IsConditionSatisfied(character, opponent))
        {
            ApplyEffects(character, opponent);
        }
    }

    protected bool IsConditionSatisfied(Character character, Character opponent)
    {
        return _condition.IsSatisfied(character, opponent);
    }

    protected void ApplyEffects(Character character, Character opponent)
    {
        foreach (var effect in _effects.GetEffects())
        {
            effect.Apply(character, opponent);
        }
    }
}