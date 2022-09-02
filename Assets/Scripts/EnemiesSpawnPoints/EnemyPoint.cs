using Entities.Enemies;
using UnityEngine;

namespace EnemiesSpawnPoints
{
    public class EnemyPoint : MonoBehaviour
    {
        private Enemy _holdEnemy;

        public bool IsFree => _holdEnemy == null;
        public Enemy HoldEnemy => _holdEnemy;
    
        public void Hold(Enemy byEnemy)
        {
            _holdEnemy = byEnemy;
            byEnemy.transform.position = transform.position;
        }

        public void UnHold() => _holdEnemy = null;
    }
}