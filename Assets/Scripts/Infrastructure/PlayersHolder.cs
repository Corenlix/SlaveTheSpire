using System.Collections.Generic;
using System.Linq;
using Entities;

namespace Infrastructure
{
    public class PlayersHolder : IPlayersHolder
    {
        public List<Player> Players => _players.ToList();

        private readonly LocationHolder _locationHolder;
        private readonly List<Player> _players = new();

        public PlayersHolder(LocationHolder locationHolder)
        {
            _locationHolder = locationHolder;
        }
        
        public void Add(Player player)
        {
            if(!Players.Contains(player))
                Players.Add(player);
            
            _players.Add(player);
            _locationHolder.Location.PlayersContainer.Add(player);
            player.Destroyed += OnPlayerDestroyed;
        }

        private void OnPlayerDestroyed(Entity player)
        {
            player.Destroyed -= OnPlayerDestroyed;
            Remove((Player) player);
        }

        public void Remove(Player player)
        {
            Players.Remove(player);
        }
    }
}