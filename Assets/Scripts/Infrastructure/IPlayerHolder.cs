using Entities;
using UnityEngine;

namespace Infrastructure
{
    public interface IPlayerHolder
    {
        Player Player { get; }
        
        void SetPlayer(Player player);
    }
}