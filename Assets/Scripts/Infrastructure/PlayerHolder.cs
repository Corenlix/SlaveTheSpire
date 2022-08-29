using Entities;

namespace Infrastructure
{
    public class PlayerHolder : IPlayerHolder
    {
        public Player Player { get; private set; }

        public void SetPlayer(Player player)
        {
            Player = player;
        }
    }
}