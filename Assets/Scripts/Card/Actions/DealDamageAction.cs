using System.Collections.Generic;

namespace Card.Actions
{
    public class DealDamageAction : ICardAction
    {
        private readonly int _damage;

        public DealDamageAction(int damage)
        {
            _damage = damage;
        }
        
        public void Activate(List<Entity> targets)
        {
            targets.ForEach(x=>x.TakeDamage(_damage));
        }
    }
}