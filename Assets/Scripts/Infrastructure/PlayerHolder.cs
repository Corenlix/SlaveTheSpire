using Entities;
using UnityEngine;

namespace Infrastructure
{
    public class PlayerHolder : IPlayerHolder
    {
        public BoundedValue Energy { get; }
        public BoundedValue Health { get; }

        public Vector3 Position => _player.transform.position;

        private Player _player;

        public PlayerHolder()
        {
            Health = new BoundedValue(15);
            Energy = new BoundedValue(3);
        }

        public void SetPlayer(Player player)
        {
            _player = player;
            player.Init(Health);
        }
    }
}