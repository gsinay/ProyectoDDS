using Fire_Emblem.Models.Characters;
using Fire_Emblem.Models.Characters.Modifiers;
using Fire_Emblem.Models.Collections;

namespace Fire_Emblem.Models.Skills.Effects.CombatModifierEffects
{
    public class PercentDamageReductionPercentReductionEffect : IEffect
    {
        private readonly double _reductionPercentage;

        public PercentDamageReductionPercentReductionEffect(double reductionPercentage)
        {
            _reductionPercentage = reductionPercentage;
        }

        public void Apply(Character character, Character opponent)
        {
            ApplyReductionToDamageEffects(opponent.CharacterModifiers.CombatModifiers.DamageReduction);
            ApplyReductionToDamageEffects(opponent.CharacterModifiers.FirstAttackModifiers.DamageReduction);
            ApplyReductionToDamageEffects(opponent.CharacterModifiers.FollowupModifiers.DamageReduction);
            ApplyReductionToDamageEffects(character.CharacterModifiers.CombatModifiers.DamageReduction);
            ApplyReductionToDamageEffects(character.CharacterModifiers.FirstAttackModifiers.DamageReduction);
            ApplyReductionToDamageEffects(character.CharacterModifiers.FollowupModifiers.DamageReduction);
        }

        private void ApplyReductionToDamageEffects(DamageReductionEffects damageReductionEffects)
        {
            damageReductionEffects.ApplyModifier(_reductionPercentage);
        }
    }
}