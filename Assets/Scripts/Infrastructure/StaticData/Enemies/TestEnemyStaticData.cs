using Entities.Enemies;
using UnityEngine;

namespace Infrastructure.StaticData.Enemies
{
    [CreateAssetMenu(menuName = "Enemy/Default Enemy")]
    public class TestEnemyStaticData : EnemyStaticData
    {
        [SerializeField] private TestEnemy _enemyPrefab;
        [SerializeField] private int _damage;
        
        public override Enemy EnemyPrefab => _enemyPrefab;
        public int Damage => _damage;
    }
}