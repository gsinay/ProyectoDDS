using Fire_Emblem_View;
using Fire_Emblem.Characters;
namespace Fire_Emblem;


public class StatBoostEffect : IEffect
{
    private readonly string _stat;
    private readonly int _amount;
    private readonly bool _applyToBoth; 
    private View _view;
    public StatBoostEffect(string stat, int amount, View view, bool applyToBoth = false)
    {
        _stat = stat;
        _amount = amount;
        _applyToBoth = applyToBoth;
        _view = view;
    }

    public void Apply(Character character, Character opponent, CombatLog combatLog)
    {
        ApplyEffect(character, combatLog);

        if (_applyToBoth)
        {
            ApplyEffect(opponent, combatLog);
        }
    }
    
    private void ApplyEffect(Character target, CombatLog combatLog)
    {
        switch (_stat)
        {
            case "Atk":
                target.AtkModifier += _amount;
                combatLog.LogBonus(target, _stat, _amount);
                break;
            case "Spd":
                target.SpdModifier += _amount;
                combatLog.LogBonus(target, _stat, _amount);
                break;
            case "Def":
                target.DefModifier += _amount;
                combatLog.LogBonus(target, _stat, _amount);
                break;
            case "Res":
                target.ResModifier += _amount;
                combatLog.LogBonus(target, _stat, _amount);
                break;
        }
    }
}