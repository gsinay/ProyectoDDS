using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects;

public interface IEffect
{
    void Apply(Character character, Character opponent);

}