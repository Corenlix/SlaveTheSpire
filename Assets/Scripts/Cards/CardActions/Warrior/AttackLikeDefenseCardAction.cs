using System.Collections.Generic;
using Entities;

namespace Cards.CardActions
{
    public class AttackLikeDefenseCardAction : ICardAction
    {
        public void Use(List<Entity> targets, Player cardOwner)
        {
            cardOwner.AttackProcessor.AttackWithoutBuffs(cardOwner.EntityHealth.Armor, targets.ToArray());
        }
    }
}