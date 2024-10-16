using Fire_Emblem.Models.Skills;
using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.Collections
{
    public class ConditionalEffectsList
    {
        private List<ConditionalEffect> _effects;

        public ConditionalEffectsList(IEnumerable<ConditionalEffect> effects)
        {
            _effects = new List<ConditionalEffect>(effects);
        }
        public IReadOnlyList<ConditionalEffect> GetConditionalEffects()
        {
            return _effects.AsReadOnly();
        }
    }
}