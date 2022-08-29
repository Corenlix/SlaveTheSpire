using System;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Enemy/Default Enemy")]
    public class DefaultEnemyStaticData : EnemyStaticData
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _damage;
        
        public override Enemy EnemyPrefab => _enemyPrefab;
        public int Damage => _damage;
    }
}