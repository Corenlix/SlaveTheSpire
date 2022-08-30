using System.Collections.Generic;
using Entities;

namespace Card
{
    public interface ICardActivator
    {
        bool IsAvailableToUse(CardHolder cardHolder);
        void Use(CardHolder cardHolder, List<Entity> targets);
    }
}