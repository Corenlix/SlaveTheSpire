using Entities;

namespace Infrastructure
{
    public class PlayerHolder : IPlayerHolder
    {
        public BoundedValue Energy { get; private set; }
        public BoundedValue Health { get; private set; }

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