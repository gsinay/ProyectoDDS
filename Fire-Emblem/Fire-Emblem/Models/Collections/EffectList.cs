using System.Collections;
using Fire_Emblem.Models.Skills.Effects;

namespace Fire_Emblem.Models.Collections
{
    public class EffectsList : IEnumerable<IEffect>
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
        
        public IEnumerator<IEffect> GetEnumerator()
        {
            return _effects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
