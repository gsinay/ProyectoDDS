using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class SandstormEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        
        double z = Math.Floor(1.5 * character.Stats.BaseStats[StatName.Def]); 
        int truncatedZ = Convert.ToInt32(z); 

       
        int atkDifference = truncatedZ - character.Stats.BaseStats[StatName.Atk];
        if (atkDifference > 0)
        {
            character.Stats.FollowupBonuses[StatName.Atk] += atkDifference;
        }
        else
        {
            int penalty = Math.Abs(atkDifference);
            character.Stats.FollowupPenalties[StatName.Atk] -= penalty;
        }
    }

}