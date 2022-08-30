using System.Collections.Generic;
using Entities;

namespace Card.Actions
{
    public interface ICardAction
    {
        void Activate(List<Entity> targets);
    }
}