using System.Collections.Generic;
using Entities;

namespace Cards
{
    public interface ICardAction
    {
        void Use(List<Entity> targets, Player cardOwner);
    }
}