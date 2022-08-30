using Entities.Enemies;
using UnityEngine;

namespace Infrastructure.StaticData.Enemies.EnemiesActions
{
    [CreateAssetMenu(menuName = "Enemy/Default Enemy")]
    public class DefaultEnemyStaticData : EnemyStaticData
    {
        [SerializeField] private Enemy _enemyPrefab;

        public override Enemy EnemyPrefab => _enemyPrefab;
    }
}