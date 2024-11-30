using Fire_Emblem.Controllers.Handlers;
using Fire_Emblem.Models.Characters;
namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects;

public class BewitchingTomeEffect : IEffect
{
    public void Apply(Character character, Character opponent)
    {
        WtbHandler wtbHandler = new();
        double triangleAdvantage = wtbHandler.GetTriangleAdvantage(character, opponent);

        var percentage = CalculatePercentage(character, opponent, triangleAdvantage);

        int hpLost = (int)(opponent.GeneralEffectiveAtk * percentage);
        opponent.CharacterModifiers.CombatModifiers.BeforeCombatHpReduction += hpLost;
    }

    private double CalculatePercentage(Character character, Character opponent, double triangleAdvantage)
    {
        return HasTriangleOrSpeedAdvantage(character, opponent, triangleAdvantage) ? 0.4 : 0.2;
    }

    private bool HasTriangleOrSpeedAdvantage(Character character, Character opponent, double triangleAdvantage)
    {
        return triangleAdvantage == 1.2 || character.GeneralEffectiveSpd > opponent.GeneralEffectiveSpd;
    }
}