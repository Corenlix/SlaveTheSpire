using System.Collections.Generic;
using Entities;

namespace Cards.CardActions
{
    public class AttackLikeDefenseCardAction : ICardAction
    {
        public void Use(List<Entity> targets, Player cardOwner)
        {
            targets.ForEach(x=>x.ApplyDamage(cardOwner.Armor));
        }
    }
}