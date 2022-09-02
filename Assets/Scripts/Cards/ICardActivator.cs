using System.Collections.Generic;
using Entities;

namespace Cards
{
    public interface ICardActivator
    {
        bool IsAvailableToUse();
        void Use(List<Entity> targets);
    }
}