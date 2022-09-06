using System.Collections.Generic;
using EnemiesSpawnPoints;
using Entities.Enemies;

namespace Infrastructure
{
    public class EnemiesHolder : EntitiesHolder<Enemy>, IEnemiesHolder
    {
        private readonly LocationHolder _locationHolder;

        public List<Enemy> Enemies => Entities;

        public EnemiesHolder(LocationHolder locationHolder)
        {
            _locationHolder = locationHolder;
        }

        protected override EntitiesContainer EntitiesContainer => _locationHolder.Location.EnemiesContainer;
    }
}