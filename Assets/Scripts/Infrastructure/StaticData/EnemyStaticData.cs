﻿using Entities;
using UnityEngine;

namespace Infrastructure.StaticData
{
    public abstract class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private EnemyId _id;
        [SerializeField] private int _maxHealth;
        
        public EnemyId Id => _id;
        public abstract Enemy EnemyPrefab { get; }
        public int MaxHealth => _maxHealth;
    }
}