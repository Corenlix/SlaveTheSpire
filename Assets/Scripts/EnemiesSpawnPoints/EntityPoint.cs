using Entities;
using UnityEngine;

namespace EnemiesSpawnPoints
{
    public class EntityPoint : MonoBehaviour
    {
        private Entity _holdEntity;

        public bool IsFree => _holdEntity == null;
        public Entity HoldEntity => _holdEntity;
    
        public void Hold(Entity entity)
        {
            _holdEntity = entity;
            entity.transform.position = transform.position;
        }

        public void UnHold() => _holdEntity = null;
    }
}