using Fire_Emblem_View;
using Fire_Emblem.Characters;
namespace Fire_Emblem;


public class StatChangeEffect : IEffect
{
    private readonly string _stat;
    private readonly int _amount;
    private readonly bool _applyToBoth;
    private readonly bool _applyToOpponent;
    private View _view;
    public StatChangeEffect(string stat, int amount, View view, bool applyToBoth = false, bool applyToOpponent = false)
    {
        _stat = stat;
        _amount = amount;
        _applyToBoth = applyToBoth;
        _applyToOpponent = applyToOpponent;
        _view = view;
    }

    public void Apply(Character character, Character opponent)
    {
        if (!_applyToOpponent)
        {
            ApplyBoostEffect(character);
            if (_applyToBoth)
                ApplyBoostEffect(opponent);
        }
        else
        {
            ApplyPenaltyEffect(opponent);
        }
    }
    
    private void ApplyBoostEffect(Character target)
    {
        switch (_stat)
        {
            case "Atk":
                target.Stats.CombatBonuses["Atk"] += _amount;
                break;
            case "Spd":
                target.Stats.CombatBonuses["Spd"] += _amount;
                break;
            case "Def":
                target.Stats.CombatBonuses["Def"] += _amount;
                break;
            case "Res":
                target.Stats.CombatBonuses["Res"] += _amount;
                break;
        }
    }

    private void ApplyPenaltyEffect(Character target)
    {
        switch (_stat)
        {
            case "Atk":
                target.Stats.CombatPenalties["Atk"] -= _amount;
                break;
            case "Spd":
                target.Stats.CombatPenalties["Spd"] -= _amount;
                break;
            case "Def":
                target.Stats.CombatPenalties["Def"] -= _amount;
                break;
            case "Res":
                target.Stats.CombatPenalties["Res"] -= _amount;
                break;
        }
    }
}