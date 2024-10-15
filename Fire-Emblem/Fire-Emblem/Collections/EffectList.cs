


using Fire_Emblem.Skills.Effects;

namespace Fire_Emblem.Collections
{
    public class EffectsList
    {
        private List<IEffect> _effects;

        public EffectsList()
        {
            _effects = new List<IEffect>();
        }

        public void AddEffect(IEffect effect)
        {
            _effects.Add(effect);
        }

        public List<IEffect> GetEffects()
        {
            return _effects;
        }
    }
}
