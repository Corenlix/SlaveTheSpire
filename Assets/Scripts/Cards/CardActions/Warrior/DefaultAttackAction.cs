using System.Collections.Generic;
using Entities;
using Infrastructure;

namespace Cards.CardActions.Warrior
{
    public class DefaultAttackAction : ICardAction
    {
        private readonly int _damage;

        public DefaultAttackAction(int damage)
        {
            _damage = damage;
        }
    
        public void Use(List<Entity> targets, Player cardOwner)
        {
            cardOwner.AttackProcessor.Attack(_damage, targets.ToArray());
        }
    }
}
