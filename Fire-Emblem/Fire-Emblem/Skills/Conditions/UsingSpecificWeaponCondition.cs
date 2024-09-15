
using Fire_Emblem.Characters;

namespace Fire_Emblem;

public class UsingSpecificWeaponCondition: ICondition
{
    private string _weapon;
    private bool _opponentUsing;

    public UsingSpecificWeaponCondition(string weapon, bool opponentUsing = false)
    {
        _weapon = weapon;
        _opponentUsing = opponentUsing;
    }
    public bool IsSatisfied(Character character, Character opponent)
    {
        if (_opponentUsing)
            return opponent.Info.Weapon == _weapon;
        return character.Info.Weapon == _weapon;
    }

}