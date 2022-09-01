using System.Collections.Generic;
using Entities;
using Infrastructure;

namespace Cards.CardActions
{
    public class ValorCardAction : ICardAction
    {
        private readonly IPlayerHolder _playerHolder;
        private readonly int _healthForDamage;
        
        private Player _cardOwner;
        private int _bonusDamageApplied;
        
        public ValorCardAction(IPlayerHolder playerHolder, int healthForDamage)
        {
            _playerHolder = playerHolder;
            _healthForDamage = healthForDamage;
        }
        
        public void Use(List<Entity> targets, Player cardOwner)
        {
            _cardOwner = cardOwner;
            _playerHolder.Player.EntityDamaged += OnEntityTakeDamage;
            _cardOwner.EntityStepStarted += OnOwnerStep;

            var damageProcessor = new ActionAfterAttacksDamageProcessor(1);
            _cardOwner.AddDamageProcessor(damageProcessor);
            damageProcessor.SetAction(() =>
            {
                cardOwner.RemoveDamageProcessor(damageProcessor);
                cardOwner.BonusDamage -= _bonusDamageApplied;
            });
        }

        private void OnOwnerStep(Entity obj)
        {
            _cardOwner.EntityStepStarted -= OnOwnerStep;
            _playerHolder.Player.EntityDamaged -= OnEntityTakeDamage;
        }

        private void OnEntityTakeDamage(Entity entity, int damage)
        {
            int bonusDamage = _healthForDamage * damage;
            _cardOwner.BonusDamage += bonusDamage;
            _bonusDamageApplied += bonusDamage;
        }
    }
}