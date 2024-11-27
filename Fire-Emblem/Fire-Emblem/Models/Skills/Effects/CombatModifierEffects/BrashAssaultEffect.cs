using Fire_Emblem.Controllers.Handlers;
using Fire_Emblem.Models.Characters;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class BrashAssaultEffect : IEffect
{
    private readonly AttackHandler _attackHandler = new();

    public void Apply(Character character, Character opponent)
    {
        int damageInflicted = getRawDamage(character, opponent);
        int damageWithPercentage = (int)(damageInflicted * 0.3);
        character.CharacterModifiers.FollowupModifiers.FlatAttackIncrement += damageWithPercentage;
    }

    public int getRawDamage(Character character, Character opponent)
    {
        int rawInflictedDamage = _attackHandler.CalculateRawInflictedDamage(opponent, character, "first");
       
        return rawInflictedDamage;
        
    }
}