using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class LaguzFriendEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        character.CharacterModifiers.CombatModifiers.ReducePercentageOfDamageReceived(0.5);
        character.Stats.NeutralizedBonuses.Neutralize(StatName.Def);
        character.Stats.NeutralizedBonuses.Neutralize(StatName.Res);
        int penaltyDef = character.Stats.BaseStats.GetBaseStat(StatName.Def) / 2;
        int penaltyRes = character.Stats.BaseStats.GetBaseStat(StatName.Res) / 2;
        character.Stats.CombatPenalties.AddPenalty(StatName.Def, penaltyDef);
        character.Stats.CombatPenalties.AddPenalty(StatName.Res, penaltyRes);
        
    }
}