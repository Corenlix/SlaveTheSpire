using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Infrastructure
{
    public class TurnResolver : ITurnResolver
    {
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly IPlayersHolder _playersHolder;

        private readonly List<Entity> _steppedEntities = new();
        private Entity _current;

        public Entity Current => _current;

        public TurnResolver(IEnemiesHolder enemiesHolder, IPlayersHolder playersHolder)
        {
            _enemiesHolder = enemiesHolder;
            _playersHolder = playersHolder;
        }

        public Entity Next()
        {
            var entities = AllEntities();
            if (entities.Count == 0)
            {
                _steppedEntities.Clear();
                entities = AllEntities();
            }
            
            Entity next = EntityWithMaxInitiative(entities);
            _current = next;
            _steppedEntities.Add(next);
            return next;
        }

        private static Entity EntityWithMaxInitiative(List<Entity> entities)
        {
            var maxInitiative = entities.Max(x => x.Initiative);
            Entity next = entities.First(x => x.Initiative == maxInitiative);
            return next;
        }

        private List<Entity> AllEntities()
        {
            var entities = _enemiesHolder.Enemies.Select(x => (Entity) x).ToList();
            entities.AddRange(_playersHolder.Players);
            entities.RemoveAll(x => _steppedEntities.Contains(x));
            return entities;
        }
    }
}