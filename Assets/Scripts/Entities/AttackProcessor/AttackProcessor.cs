using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Entities
{
    public class AttackProcessor
    {
        private readonly List<DamageProcessor> _damageProcessors = new();
        public int BonusDamage { get; set; } = 0;
        public float DamageMultiplier { get; set; } = 1;

        public void AddDamageProcessor(DamageProcessor damageProcessor) => _damageProcessors.Add(damageProcessor);
        public void RemoveDamageProcessor(DamageProcessor damageProcessor) => _damageProcessors.Remove(damageProcessor);
        
        public void Attack(List<Entity> targets, int damage)
        {
            damage = Mathf.RoundToInt((damage + BonusDamage)*DamageMultiplier);
            _damageProcessors.ForEach(x=>damage=x.DamageProcess(damage));
            _damageProcessors.ToList().ForEach(x=>x.PostDamageProcess(damage*targets.Count));
            targets.ForEach(x=>x.ApplyDamage(damage));
        }
    }
}