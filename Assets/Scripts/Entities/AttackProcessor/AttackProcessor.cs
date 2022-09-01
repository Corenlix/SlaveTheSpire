using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public class AttackProcessor
    {
        public event Action<int> Attacked;
        
        private readonly List<DamageProcessor> _damageProcessors = new();
        public int BonusDamage { get; set; } = 0;
        public float DamageMultiplier { get; set; } = 1;

        public void AddDamageProcessor(DamageProcessor damageProcessor) => _damageProcessors.Add(damageProcessor);
        public void RemoveDamageProcessor(DamageProcessor damageProcessor) => _damageProcessors.Remove(damageProcessor);
        
        public void Attack(List<Entity> targets, int damage)
        {
            damage = Mathf.RoundToInt((damage + BonusDamage)*DamageMultiplier);
            _damageProcessors.ForEach(x=>damage=x.DamageProcess(damage));
            targets.ForEach(x=>x.EntityHealth.ApplyDamage(damage));
            int totalDamage = damage * targets.Count;
            Attacked?.Invoke(totalDamage);
        }
    }
}