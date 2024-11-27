using Fire_Emblem.Controllers.Handlers;
using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Names;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class BewitchingTomeEffect : IEffect
{

    public void Apply(Character character, Character opponent)
    {
        double percentage;
        WtbHandler wtbHandler = new();
        double triangleAdvantage = wtbHandler.GetTriangleAdvantage(character, opponent);
        if (triangleAdvantage == 1.2 || character.GeneralEffectiveSpd > opponent.GeneralEffectiveSpd)
            percentage = 0.4;
        else
            percentage = 0.2;
        int hpLost = (int) (opponent.GeneralEffectiveAtk* percentage);
        opponent.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction += hpLost;

    }
}