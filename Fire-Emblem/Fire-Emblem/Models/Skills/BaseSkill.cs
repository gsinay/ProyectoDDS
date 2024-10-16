using Fire_Emblem.Characters;
using Fire_Emblem.Collections;
using Fire_Emblem.Skills.Conditions;

namespace Fire_Emblem.Skills;

public abstract class BaseSkill : ISkill
{
    public string Name { get; }
    public string Description { get; }
    private readonly ICondition? _condition;
    private readonly EffectsList? _effects;

    protected BaseSkill(string name, string description, ICondition condition, EffectsList effects)
    {
        Name = name;
        Description = description;
        _condition = condition;  
        _effects = effects;
    }
    
    protected BaseSkill(string name, string description)
    {
        Name = name;
        Description = description;
      
    }

    public virtual void ApplySkill(Character character, Character opponent)
    {
        if (IsConditionSatisfied(character, opponent))
        {
            ApplyEffects(character, opponent);
        }
    }

    private bool IsConditionSatisfied(Character character, Character opponent)
    {
        return _condition != null && _condition.IsSatisfied(character, opponent);
    }

    private void ApplyEffects(Character character, Character opponent)
    {
        if (_effects == null) return;
        foreach (var effect in _effects.GetEffects())
        {
            effect.Apply(character, opponent);
        }
    }
}