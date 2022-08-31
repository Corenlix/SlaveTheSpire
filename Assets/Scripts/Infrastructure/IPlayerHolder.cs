using Entities;

namespace Infrastructure
{
    public interface IPlayerHolder
    {
        Player Player { get; }
        
        void SetPlayer(Player player);
    }
}