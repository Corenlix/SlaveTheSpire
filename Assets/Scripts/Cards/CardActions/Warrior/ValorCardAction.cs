using System.Collections.Generic;
using Entities;
using Infrastructure;

namespace Cards.CardActions
{
    public class ValorCardAction : ICardAction
    {
        private readonly IEnemiesHolder _enemiesHolder;
        private readonly int _healthForDamage;
        
        private Player _cardOwner;
        private int _bonusDamageApplied;
        
        public ValorCardAction(IEnemiesHolder enemiesHolder, int healthForDamage)
        {
            _enemiesHolder = enemiesHolder;
            _healthForDamage = healthForDamage;
        }
        
        public void Use(List<Entity> targets, Player cardOwner)
        {
            _cardOwner = cardOwner;
            _enemiesHolder.Enemies.ForEach(x=>x.AttackProcessor.Attacked+=OnEnemyAttacked);
            cardOwner.EntityStepStarted += OnOwnerStep;
            cardOwner.AttackProcessor.Attacked += OnPlayerAttacked;
        }

        private void OnPlayerAttacked(int damage)
        {
            _cardOwner.AttackProcessor.BonusDamage -= _bonusDamageApplied;
        }

        private void OnEnemyAttacked(int damage)
        {
            int bonusDamage = _healthForDamage * damage;
            _cardOwner.AttackProcessor.BonusDamage += bonusDamage;
            _bonusDamageApplied += bonusDamage;
        }

        private void OnOwnerStep(Entity obj)
        {
            _cardOwner.EntityStepStarted -= OnOwnerStep;
            _enemiesHolder.Enemies.ForEach(x=>x.AttackProcessor.Attacked-=OnEnemyAttacked);
        }
    }
}