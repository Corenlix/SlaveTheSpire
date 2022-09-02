using UnityEngine;

namespace Entities
{
    public class MultiplyDamageProcessor : DamageProcessor
    {
        private readonly float _multiplier;
        
        public MultiplyDamageProcessor(float multiplier)
        {
            _multiplier = multiplier;
        }

        public int DamageProcess(int damage)
        {
            return Mathf.RoundToInt(damage * _multiplier);
        }
    }
}