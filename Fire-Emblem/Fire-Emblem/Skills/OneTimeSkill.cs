using Fire_Emblem;
using Fire_Emblem.Characters;

public class OneTimeSkill : ISkill
{
    public string Name { get; }
    public string Description { get; }
    private readonly ICondition _condition;  
    private readonly List<IEffect> _effects;

    public OneTimeSkill(string name, string description, ICondition condition, List<IEffect> effects)
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