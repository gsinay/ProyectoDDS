using Fire_Emblem_View;
using Fire_Emblem.Characters;
namespace Fire_Emblem;


public class StatChangeEffect : IEffect
{
    private readonly StatName _stat;
    private readonly int _amount;
    private readonly bool _applyToBoth;
    private readonly bool _applyToOpponent;
   
    public StatChangeEffect(StatName stat, int amount, bool applyToBoth = false, bool applyToOpponent = false)
    {
        _stat = stat;
        _amount = amount;
        _applyToBoth = applyToBoth;
        _applyToOpponent = applyToOpponent;
        
    }

    public void Apply(Character character, Character opponent)
    {
        if (_applyToOpponent)
        {
            ApplyPenaltyEffect(opponent);
            return;
        }

        if (_amount > 0)
        {
            ApplyBoostEffect(character);
            if (_applyToBoth)
                ApplyBoostEffect(opponent);
        }
        else
        {
            ApplyPenaltyEffect(character);
            if (_applyToBoth)
                ApplyPenaltyEffect(opponent);
        }
    }

    
    private void ApplyBoostEffect(Character target)
    {
        switch (_stat)
        {
            case StatName.Atk:
                target.Stats.CombatBonuses[StatName.Atk] += _amount;
                break;
            case StatName.Spd:
                target.Stats.CombatBonuses[StatName.Spd] += _amount;
                break;
            case StatName.Def:
                target.Stats.CombatBonuses[StatName.Def] += _amount;
                break;
            case StatName.Res:
                target.Stats.CombatBonuses[StatName.Res] += _amount;
                break;
        }
    }

    private void ApplyPenaltyEffect(Character target)
    {
        switch (_stat)
        {
            case StatName.Atk:
                target.Stats.CombatPenalties[StatName.Atk] += _amount;
                break;
            case StatName.Spd:
                target.Stats.CombatPenalties[StatName.Spd] += _amount;
                break;
            case StatName.Def:
                target.Stats.CombatPenalties[StatName.Def] += _amount;
                break;
            case StatName.Res:
                target.Stats.CombatPenalties[StatName.Res] += _amount;
                break;
        }
    }
}