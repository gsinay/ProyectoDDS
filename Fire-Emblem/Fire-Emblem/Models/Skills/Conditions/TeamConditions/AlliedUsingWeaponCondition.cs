using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Collections;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Conditions.TeamConditions;

public class AlliedUsingWeaponCondition : ICondition
{
   private readonly WeaponName _weaponName;

   public AlliedUsingWeaponCondition(WeaponName weaponName)
   {
      _weaponName = weaponName;
   }
   public bool IsSatisfied(Character character, Character opponent)
   {
      var teammates = character.OwnerPlayer.Characters
         .Where(c => c != character && c.IsAlive());

      foreach (var teammate in teammates)
      {
         if (teammate.Info.Weapon == _weaponName)
            return true;
      }
      return false;
   }
}