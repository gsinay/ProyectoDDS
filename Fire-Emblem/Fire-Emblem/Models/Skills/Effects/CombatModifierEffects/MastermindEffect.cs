using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class MastermindEffect :  IEffect
{

    public void Apply(Character character, Character opponent)
    {
        int totalBonuses = (int)(GetTotalBonuses(character) * 0.8);
        int totalPenalties = (int)(GetTotalPenalties(opponent) * 0.8);
        int totalIncrement = totalBonuses + totalPenalties;
        character.CharacterModifiers.CombatModifiers.FlatAttackIncrement += totalIncrement;

    }

    private int GetTotalBonuses(Character character)
    {
        int totalBonuses = 0;

        var bonuses = character.Stats.CombatBonuses.GetAllBonuses();
        totalBonuses += character.Stats.NeutralizedBonuses.IsNeutralized(StatName.Atk) ? 0 : bonuses[StatName.Atk];
        totalBonuses += character.Stats.NeutralizedBonuses.IsNeutralized(StatName.Spd) ? 0 : bonuses[StatName.Spd];
        totalBonuses += character.Stats.NeutralizedBonuses.IsNeutralized(StatName.Def) ? 0 : bonuses[StatName.Def];
        totalBonuses += character.Stats.NeutralizedBonuses.IsNeutralized(StatName.Res) ? 0 : bonuses[StatName.Res];
        Console.WriteLine($"the total bonuses is {totalBonuses}");
        return totalBonuses;
    }
    
    private int GetTotalPenalties(Character character)
    {
        int totalPenalties = 0;
        var penalties = character.Stats.CombatPenalties.GetAllPenalties();
        totalPenalties += character.Stats.NeutralizedPenalties.IsNeutralized(StatName.Atk) ? 0 : penalties[StatName.Atk];
        totalPenalties += character.Stats.NeutralizedPenalties.IsNeutralized(StatName.Spd) ? 0 : penalties[StatName.Spd];
        totalPenalties += character.Stats.NeutralizedPenalties.IsNeutralized(StatName.Def) ? 0 : penalties[StatName.Def];
        totalPenalties += character.Stats.NeutralizedPenalties.IsNeutralized(StatName.Res) ? 0 : penalties[StatName.Res];
        return totalPenalties;
    }

}