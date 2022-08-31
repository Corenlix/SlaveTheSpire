using System.Collections.Generic;
using Entities;

namespace Cards.Actions
{
    public interface ICardAction
    {
        void Activate(List<Entity> targets);
    }
}