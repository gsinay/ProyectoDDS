using Fire_Emblem;
using Fire_Emblem.Characters;

public class Skill : ISkill
{
    public string Name { get; }
    public string Description { get; }
    private readonly List<ICondition> _conditions;
    private readonly List<IEffect> _effects;
    

    public Skill(string name, string description, List<ICondition> conditions, List<IEffect> effects)
    {
        Name = name;
        Description = description;
        _conditions = conditions;
        _effects = effects;
    }

    public void ApplyEffect(Character character, Character opponent, CombatLog combatLog)
    {
        if (_conditions.All(condition => condition.IsSatisfied(character, opponent)))
        {
            foreach (var effect in _effects)
            {
                effect.Apply(character, opponent,  combatLog);
            }
        }
    }
    
}