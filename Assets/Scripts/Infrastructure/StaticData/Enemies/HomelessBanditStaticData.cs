using Entities.Enemies;
using UnityEngine;

namespace Infrastructure.StaticData.Enemies
{
    [CreateAssetMenu(menuName = "Enemy/Homeless Bandit")]
    public class HomelessBanditStaticData : EnemyStaticData
    {
        [SerializeField] private HomelessBandit _enemyPrefab;
        [SerializeField] private int _damage;
        public override Enemy EnemyPrefab => _enemyPrefab;

        public int Damage => _damage;
    }
}
