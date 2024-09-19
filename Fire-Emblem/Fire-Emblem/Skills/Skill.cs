using Fire_Emblem.Characters;
using Fire_Emblem.Combat;
using Fire_Emblem.Skills.Conditions;
using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.Skills;

public class Skill : ISkill
{
    public string Name { get; }
    public string Description { get; }
    private readonly ICondition _condition;  
    private readonly List<IEffect> _effects;

    public Skill(string name, string description, ICondition condition, List<IEffect> effects)
    {
        Name = name;
        Description = description;
        _condition = condition;  
        _effects = effects;
    }

    public void ApplyEffect(Character character, Character opponent, CombatLog combatLog)
    {
        if (_condition.IsSatisfied(character, opponent))  
        {
            foreach (var effect in _effects)
            {
                effect.Apply(character, opponent);
            }
        }
    }
}