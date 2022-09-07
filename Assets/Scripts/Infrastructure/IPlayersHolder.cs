using System.Collections.Generic;
using Entities;
using Infrastructure.Progress;

namespace Infrastructure
{
    public interface IPlayersHolder : IProgressClient
    {
        List<Player> Players { get; }
        
        void Add(Player player);

        void Remove(Player player);
    }
}