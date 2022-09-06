using System.Collections.Generic;
using System.Linq;
using EnemiesSpawnPoints;
using Entities;

namespace Infrastructure
{
    public class PlayersHolder : EntitiesHolder<Player>, IPlayersHolder
    {
        private readonly LocationHolder _locationHolder;

        public List<Player> Players => Entities;

        protected override EntitiesContainer EntitiesContainer => _locationHolder.Location.PlayersContainer;

        public PlayersHolder(LocationHolder locationHolder)
        {
            _locationHolder = locationHolder;
        }
    }
}