using System.Collections.Generic;

namespace Card.Actions
{
    public interface ICardAction
    {
        void Activate(List<Entity> targets);
    }
}