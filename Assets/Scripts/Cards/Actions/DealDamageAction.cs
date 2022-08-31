using System.Collections.Generic;
using Entities;
using Infrastructure.Factories;

namespace Cards.Actions
{
    public class DealDamageAction : ICardAction
    {
        private readonly int _damage;
        private readonly IGameFactory _gameFactory;

        public DealDamageAction(int damage, IGameFactory gameFactory)
        {
            _damage = damage;
            _gameFactory = gameFactory;
        }
        
        public void Activate(List<Entity> targets)
        {
            foreach (var target in targets)
            {
                target.TakeDamage(_damage);
                _gameFactory.SpawnDamageEffect(_damage, target.transform.position);
            }
        }
    }
}