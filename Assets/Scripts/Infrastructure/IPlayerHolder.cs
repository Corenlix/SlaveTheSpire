using Entities;

namespace Infrastructure
{
    public interface IPlayerHolder
    {
        BoundedValue Energy { get; }
        BoundedValue Health { get; }
        void SetPlayer(Player player);
    }
}