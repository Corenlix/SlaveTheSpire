using Entities;

namespace Infrastructure
{
    public interface ITurnResolver
    {
        Entity Current { get; }
        Entity Next();
    }
}