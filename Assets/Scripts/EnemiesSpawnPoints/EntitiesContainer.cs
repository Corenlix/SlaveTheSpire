using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using Entities.Enemies;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace EnemiesSpawnPoints
{
    public class EntitiesContainer : MonoBehaviour
    {
        [SerializeField] private List<EntityPoint> _entityPoints;

        public bool HasFreePoint()
        {
            return GetFreePoint() != null;
        }

        public void Add(Entity entity)
        {
            entity.transform.SetParent(transform);
            
            _entityPoints.Shuffle();
            EntityPoint freePoint = GetFreePoint();
            freePoint.Hold(entity);
            
            entity.Destroyed += Remove;
        }
    
        public void Remove(Entity entity) {
            GetPointForEnemy(entity)?.UnHold();
            entity.Destroyed -= Remove;
        }

        private EntityPoint GetFreePoint()
        {
            return _entityPoints.FirstOrDefault(x => x.IsFree);
        }

        private EntityPoint GetPointForEnemy(Entity enemy)
        {
            return _entityPoints.FirstOrDefault(x => x.HoldEntity == enemy);
        }
    }
}