using Entities;

namespace Infrastructure
{
    public interface IPlayerHolder
    {
        BoundedValue Energy { get; }
        void SetPlayer(Player player);
    }
}