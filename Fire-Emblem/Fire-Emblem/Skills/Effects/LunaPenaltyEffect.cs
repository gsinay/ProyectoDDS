using Fire_Emblem.Characters;

namespace Fire_Emblem.Skills.Effects;

public class LunaPenaltyEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        // Otorgarle al oponente un penalty para el primer ataque de su def y res a la mitad de esos stats base
        opponent.Stats.FirstAttackPenalties["Def"] -= opponent.Stats.BaseStats["Def"] / 2;
        opponent.Stats.FirstAttackPenalties["Res"] -= opponent.Stats.BaseStats["Res"] / 2;
        
    }
}