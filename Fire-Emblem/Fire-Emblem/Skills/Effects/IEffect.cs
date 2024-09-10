using Fire_Emblem;
using Fire_Emblem.Characters;

public interface IEffect
{
    void Apply(Character character, Character opponent);
}