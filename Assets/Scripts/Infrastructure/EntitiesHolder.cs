using System.Collections.Generic;
using System.Linq;
using EnemiesSpawnPoints;
using Entities;

namespace Infrastructure
{
    public abstract class EntitiesHolder<T> where T : Entity
    {
        protected List<T> Entities => _entities.ToList();
        protected abstract EntitiesContainer EntitiesContainer { get; }
        private readonly List<T> _entities = new();

        public void Add(T entity)
        {
            if (_entities.Contains(entity))
                return;
            
            _entities.Add(entity);
            EntitiesContainer.Add(entity);
            entity.Destroyed += OnEntityDestroyed;
            entity.PreDestroyed += OnEntityDestroyed;
        }

        private void OnEntityDestroyed(Entity entity)
        {
            entity.Destroyed -= OnEntityDestroyed;
            entity.PreDestroyed -= OnEntityDestroyed;
            Remove((T) entity);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }
    }
}