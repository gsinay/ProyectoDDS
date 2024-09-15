using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class LunaPenaltyEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        // Otorgarle al oponente un penalty para el primer ataque de su def y res a la mitad de esos stats base
        opponent.Stats.FirstAttackPenalties[StatName.Def] -= opponent.Stats.BaseStats[StatName.Def] / 2;
        opponent.Stats.FirstAttackPenalties[StatName.Res] -= opponent.Stats.BaseStats[StatName.Res] / 2;
        
    }
}