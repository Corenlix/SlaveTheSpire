using System.Collections.Generic;
using Entities;

namespace Cards.CardActions
{
    public class DrinkBeerCardAction : ICardAction
    {
        private readonly int _damageToOwner;
        private readonly int _bonusDamage;
        private Player _cardOwner;

        public DrinkBeerCardAction(int damageToOwner, int bonusDamage)
        {
            _damageToOwner = damageToOwner;
            _bonusDamage = bonusDamage;
        }

        public void Use(List<Entity> targets, Player cardOwner)
        {
            _cardOwner = cardOwner;
            cardOwner.EntityHealth.ApplyDamage(_damageToOwner);
            cardOwner.AttackProcessor.BonusDamage += _bonusDamage;
            cardOwner.AttackProcessor.Attacked += OnAttack;
        }

        private void OnAttack(int obj)
        {
            _cardOwner.AttackProcessor.BonusDamage -= _bonusDamage;
            _cardOwner.AttackProcessor.Attacked -= OnAttack;
        }
    }
}