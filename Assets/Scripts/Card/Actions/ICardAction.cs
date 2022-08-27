using System.Collections.Generic;
using Entities;
using Zenject;

namespace Card.Actions
{
    public interface ICardAction
    {
        void Activate(List<Entity> targets);
    }
}