﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public int Attack(int damage, params Entity[] targets)
        {
            damage = ProcessDamage(damage, targets.Length);

            var totalDamage = targets.Sum(target => target.EntityHealth.ApplyDamage(damage));
            Attacked?.Invoke(totalDamage);
            return totalDamage;
        }

        public int AttackWithoutBuffs(int damage, params Entity[] targets)
        {
            var totalDamage = targets.Sum(target => target.EntityHealth.ApplyDamage(damage));
            Attacked?.Invoke(damage);
            return totalDamage;
        }

        private int ProcessDamage(int damage, int targetsCount)
        {
            damage = Mathf.RoundToInt((damage + (float) BonusDamage / targetsCount) * DamageMultiplier);
            _damageProcessors.ForEach(x => damage = x.DamageProcess(damage));
            return damage;
        }
    }
}