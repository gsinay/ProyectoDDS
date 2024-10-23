using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Controllers.Handlers;

public class WtbHandler
{
    
    private const double _AdvantageWTB = 1.2;
    private const double _DisadvantageWTB = 0.8;
    
    public double GetTriangleAdvantage(Character attacker, Character defender)
    {
        if (HasAdvantage(attacker, defender)) return _AdvantageWTB; 
        if (HasDisadvantage(attacker, defender)) return _DisadvantageWTB;
        return 1;
    }
    
    private bool HasAdvantage(Character attacker, Character defender)
    {
        return GetAdvantagedWeapon(attacker.Info.Weapon) == defender.Info.Weapon;
    }

    private bool HasDisadvantage(Character attacker, Character defender)
    {
        return GetAdvantagedWeapon(defender.Info.Weapon) == attacker.Info.Weapon;
    }
    
    private WeaponName GetAdvantagedWeapon(WeaponName weapon)
    {
        return weapon switch
        {
            WeaponName.Sword => WeaponName.Axe,
            WeaponName.Axe => WeaponName.Lance,
            WeaponName.Lance => WeaponName.Sword,
            _ => WeaponName.None
        };
    }
}