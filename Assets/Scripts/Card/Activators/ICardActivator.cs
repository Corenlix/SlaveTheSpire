using System.Collections.Generic;
using Entities;

namespace Card
{
    public interface ICardActivator
    {
        bool IsAvailableToUse();
        void Use(List<Entity> targets);
    }
}