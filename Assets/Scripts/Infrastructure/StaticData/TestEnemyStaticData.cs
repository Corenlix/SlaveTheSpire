using Entities;
using UnityEngine;

namespace Infrastructure.StaticData
{
    [CreateAssetMenu(menuName = "Enemy/Test Enemy")]
    public class TestEnemyStaticData : EnemyStaticData
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private string _name;
        
        public override Enemy EnemyPrefab => _enemyPrefab;
        public string Name => _name;
    }
}