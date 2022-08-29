using Entities;
using UnityEngine;

namespace Infrastructure
{
    public interface IPlayerHolder
    {
        BoundedValue Energy { get; }
        BoundedValue Health { get; }
        Vector3 Position { get; }
        
        void SetPlayer(Player player);
    }
}