namespace Fire_Emblem.Models.Collections
{
    public class DamageReductionEffects
    {
        private readonly List<double> effects = new List<double>();

        public double GetCombinedReduction()
        {
            double combinedDamageReceived = 1.0;
            foreach (var reduction in effects)
            {
                combinedDamageReceived *= (1 - reduction);
            }
            return 1 - combinedDamageReceived;
        }

        public void AddEffect(double reductionPercentage)
        {
            effects.Add(reductionPercentage);
        }

        public void ApplyModifier(double modifier)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                effects[i] *= (1 - modifier);
            }
        }

        public void Reset()
        {
            effects.Clear();
        }
    }
}