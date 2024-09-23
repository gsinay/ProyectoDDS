using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public interface IEffect
{
    void Apply(Character character, Character opponent);

}