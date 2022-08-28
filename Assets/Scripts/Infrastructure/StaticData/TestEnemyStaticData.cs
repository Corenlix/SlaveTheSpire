using Entities;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Enemy/Test Enemy")]
    public class TestEnemyStaticData : EnemyStaticData
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private int _damage;
        
        public override Enemy EnemyPrefab => _enemyPrefab;
        public int Damage => _damage;
    }
}