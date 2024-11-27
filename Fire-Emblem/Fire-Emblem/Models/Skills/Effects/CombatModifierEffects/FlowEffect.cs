using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class FlowEffect: IEffect
{
    private readonly StatName _statUsedForComparison;
    private readonly double _percentage;
    
    public FlowEffect(StatName statUsedForComparison, double percentage)
    {
        _statUsedForComparison = statUsedForComparison;
        _percentage = percentage;
    }
    
    public void Apply(Character character, Character opponent)
    {
        int characterStat = character.GetGeneralStat(_statUsedForComparison);
        int rivalStat = opponent.GetGeneralStat(_statUsedForComparison);
        int deltaQuantity = (int)((characterStat - rivalStat) * _percentage);
        deltaQuantity = Math.Max(0, Math.Min(7, deltaQuantity));

        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += deltaQuantity;
        character.CharacterModifiers.CombatModifiers.FlatDamageReduction += deltaQuantity;

    }
    
   

    
}