using Entities;

namespace Infrastructure
{
    public class PlayerHolder : IPlayerHolder
    {
        public BoundedValue Energy { get; private set; }
        private BoundedValue _health;

        private Player _player;

        public PlayerHolder()
        {
            _health = new BoundedValue(5);
            Energy = new BoundedValue(3);
        }

        public void SetPlayer(Player player)
        {
            _player = player;
            player.Init(_health);
        }
    }
}