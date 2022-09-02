using System.Collections.Generic;
using System.Linq;
using Entities;
using Infrastructure;

namespace Cards.CardActions
{
    public class AoeCardAction : ICardAction
    {
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly int _damage;

        public AoeCardAction(IEnemiesHolder enemiesHolder, int damage)
        {
            _enemiesHolder = enemiesHolder;
            _damage = damage;
        }

        public void Use(List<Entity> targets, Player cardOwner)
        {
            var allEnemies = _enemiesHolder.Enemies.Select(x => (Entity) x);
            cardOwner.AttackProcessor.Attack(_damage, allEnemies.ToArray());
        }
    }
}