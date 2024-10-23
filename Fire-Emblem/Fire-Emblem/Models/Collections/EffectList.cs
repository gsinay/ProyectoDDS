using Fire_Emblem.Models.Skills.Effects;

namespace Fire_Emblem.Models.Collections
{
    public class EffectsList
    {
        private List<IEffect> _effects;

        public EffectsList(IEnumerable<IEffect> effects)
        {
            _effects = new List<IEffect>(effects);
        }
        

        public IReadOnlyList<IEffect> GetEffects()
        {
            return _effects.AsReadOnly();
        }
    }
}
