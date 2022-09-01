using System.Collections.Generic;
using Entities;
using Infrastructure.StaticData.Buffs;

namespace Cards.CardActions
{
    public class DrinkBeerCardAction : ICardAction
    {
        private readonly int _damageToOwner;
        private readonly int _bonusDamage;

        public DrinkBeerCardAction(int damageToOwner, int bonusDamage)
        {
            _damageToOwner = damageToOwner;
            _bonusDamage = bonusDamage;
        }

        public void Use(List<Entity> targets, Player cardOwner)
        {
            cardOwner.ApplyDamage(_damageToOwner);
            cardOwner.BonusDamage += _bonusDamage;
            var damageProcessor = new ActionAfterAttacksDamageProcessor(1);
            damageProcessor.SetAction(() =>
            {
                cardOwner.RemoveDamageProcessor(damageProcessor);
                cardOwner.BonusDamage -= _bonusDamage;
            });
            cardOwner.AddDamageProcessor(damageProcessor);
        }
    }
}